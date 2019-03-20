using System;
using System.Collections.Generic;
using UniRx.Async;
using UniRx;
using UnityEngine;
using System.Threading;
using GameMain.Mgr;
using System.Reflection;
using System.Linq;
using System.IO;

namespace GameMain.Net
{
	public class Client : MonoSingleton<Client>
	{
		//每次发送request自增
		private static int RpcId { get; set; }
		//request回调
		private readonly Dictionary<int, Action<IResponse>> requestCallback = new Dictionary<int, Action<IResponse>>();
		//继承IMessage
		private readonly DoubleMap<int, Type> msgTypeMap = new DoubleMap<int, Type>();
		//private readonly Dictionary<ushort, IMessage> msgDict = new Dictionary<ushort, IMessage>();
		//所有继承IResponse
		private readonly DoubleMap<int, Type> requestTypeMap = new DoubleMap<int, Type>();
		//private readonly Dictionary<ushort, IResponse> responseDict = new Dictionary<ushort, IResponse>();
		//所有继承IRequest
		private readonly DoubleMap<int, Type> responseTypeMap = new DoubleMap<int, Type>();
		//private readonly Dictionary<ushort, IRequest> requestDict = new Dictionary<ushort, IRequest>();
		//所有继承IMsgHandler
		private readonly Dictionary<int, IMsgHandler> handlerDict = new Dictionary<int, IMsgHandler>();
		//MyProtocolBytes对象池
		private Roslyn.ObjectPool<ProtocolBytes> _pool;

		protected override void Awake()
		{
			base.Awake();
			this.BindMsgAndHandler();
		}

		public void Close()
		{
			foreach (Action<IResponse> action in this.requestCallback.Values)
			{
				action.Invoke(new ResponseMessage { Error = -1 });
			}
			this.requestCallback.Clear();

			requestTypeMap.Clear();
			//responseDict.Clear();
			responseTypeMap.Clear();
			//requestDict.Clear();
			msgTypeMap.Clear();
			//msgDict.Clear();
			handlerDict.Clear();

			_pool = null;
		}

		/// <summary>
		/// 根据attribute注入
		/// </summary>
		private void BindMsgAndHandler()
		{
			Assembly ass = this.GetType().Assembly;
			Type[] types = ass.GetTypes().Where(x =>
			{
				var filter = x.IsAbstract == false;
				return filter;
			}
			).ToArray();

			//bind Meesage
			foreach (var t in types)
			{
				var msgAtt = t.GetCustomAttribute<MessageAttribute>();
				if (msgAtt != null)
				{
					var isMesg = t.GetInterfaces().Contains(typeof(IMessage));
					if (isMesg)
					{
						msgTypeMap.Add(msgAtt.Opcode, t);
						//msgDict.Add(msgAtt.Opcode, Activator.CreateInstance(t) as IMessage);

						var isRequest = t.GetInterfaces().Contains(typeof(IRequest));
						var isResponse = t.GetInterfaces().Contains(typeof(IResponse));
						if (isRequest)
						{
							requestTypeMap.Add(msgAtt.Opcode, t);
							//requestDict.Add(msgAtt.Opcode, Activator.CreateInstance(t) as IRequest);
						}
						else if (isResponse)
						{
							requestTypeMap.Add(msgAtt.Opcode, t);
							//responseDict.Add(msgAtt.Opcode, Activator.CreateInstance(t) as IResponse);
						}
					}
				}
			}

			//bind handler
			foreach (var t in types)
			{
				var handlerAtt = t.GetCustomAttribute<MessageHandlerAttribute>();
				if (handlerAtt != null)
				{
					IMsgHandler msgHandler = Activator.CreateInstance(t) as IMsgHandler;
					if (msgHandler == null)
					{
						Debug.LogError($"message handler {t.Name} 需要继承 IMsgHandler");
						continue;
					}

					Type messageType = msgHandler.GetMessageType();

					if (this.msgTypeMap.ContainsValue(messageType))
					{
						var opcode = this.msgTypeMap.GetKeyByValue(messageType);
						handlerDict.Add((ushort)opcode, Activator.CreateInstance(t) as IMsgHandler);
					}
				}
			}
		}

		

		/// <summary>
		/// 发送Request
		/// </summary>
		/// <param name="request"></param>
		/// <param name="cancellationToken"></param>
		/// <returns>返回一个Response Task</returns>
		public UniTask<IResponse> Call(IRequest request, CancellationToken? cancellationToken = null)
		{
			int rpcId = ++RpcId;
			var res = _Wrapper();
			request.RpcId = rpcId;
			this.SendRequestCore(request);
			return res;

			UniTask<IResponse> _Wrapper()
			{
				var utcs = new UniTaskCompletionSource<IResponse>();

				this.requestCallback[rpcId] = (response) =>
				{
					try
					{
						utcs.TrySetResult(response);
					}
					catch (Exception e)
					{
						utcs.TrySetException(e);
					}
				};

				if (cancellationToken.HasValue)
				{
					cancellationToken.Value.Register(() =>
					{
						this.requestCallback.Remove(rpcId);
						utcs.TrySetCanceled();
					});
				}

				return utcs.Task; //return UniTask<int>
			}
		}

		private void SendRequestCore(IRequest msg)
		{
			_pool = _pool ?? new Roslyn.ObjectPool<ProtocolBytes>(() => new ProtocolBytes());
			ProtocolBytes p = _pool.Allocate();

			if (this.msgTypeMap.ContainsValue(msg.GetType()))
			{
				var opcode = this.msgTypeMap.GetKeyByValue(msg.GetType());
				p.AddInt(opcode);
				p.AddInt(msg.RpcId);

				var temp = msg.Bytes;

				p.AddBytes(msg.Bytes);

				//NetMgr.srvConn.Send(p);
			}

			_pool.Free(p);
		}

		/// <summary>
		/// 发送Message
		/// </summary>
		/// <param name="msg"></param>
		public void SendMessage(IMessage msg)
		{
			switch (msg)
			{
				case IRequest request:
					Debug.LogError("IRequest用SendRequestCore发送");
					return;
				case IResponse request:
					Debug.LogError("IResponse只能接收");
					return;
			}

			_pool = _pool ?? new Roslyn.ObjectPool<ProtocolBytes>(() => new ProtocolBytes());
			ProtocolBytes p = _pool.Allocate();

			if (this.msgTypeMap.ContainsValue(msg.GetType()))
			{
				var opcode = this.msgTypeMap.GetKeyByValue(msg.GetType());

				p.AddInt(opcode);
				p.AddInt(-1);
				p.AddBytes(msg.Bytes);

				NetMgr.srvConn.Send(p);
			}
		}

		public void Test()
		{
			Debug.Log("Test from client");
			_pool = _pool ?? new Roslyn.ObjectPool<ProtocolBytes>(() => new ProtocolBytes());
			ProtocolBytes p = new ProtocolBytes();

			var data = new S2C_LoginData { isPassed = true, msg = "Test", position = new Vector3(2, 3, 1) };
			var test = new R2C_Login { data = data };
			if (this.msgTypeMap.ContainsValue(test.GetType()))
			{
				var opcode = this.msgTypeMap.GetKeyByValue(test.GetType());

				p.AddInt(opcode);
				p.AddInt(1);
				p.AddBytes(test.Bytes);

				OnReceiveMessage(p);
			}

			_pool.Free(p);
		}

		/// <summary>
		/// 接受Message
		/// </summary>
		/// <param name="bytes"></param>
		public void OnReceiveMessage(ProtocolBase proto)
		{
			ProtocolBytes bytes = (ProtocolBytes)proto;

			var start = 0;
			var opcode = bytes.GetInt(start, ref start);
			//是message,交给对应msgHandler
			if (this.msgTypeMap.ContainsKey(opcode))
			{
				var type = this.msgTypeMap.GetValueByKey(opcode);
				if (this.handlerDict.TryGetValue(opcode, out var handler))
				{
					var msg = Activator.CreateInstance(type);
					handler.Handle(msg);
				}
				//Debug.Log($"没有对应的msgHandler {type}");

				//是response,invoke委托
				var rpcID = bytes.GetInt(start, ref start);
				if (this.requestCallback.TryGetValue(rpcID, out var action))
				{
					this.requestCallback.Remove(rpcID);

					if (!(Activator.CreateInstance(type) is IResponse response))
					{
						return;
					}

					var temp = bytes.GetBytes(start);
					response.Bytes = temp;

					action(response);
				}
			}
		}
	}
}
