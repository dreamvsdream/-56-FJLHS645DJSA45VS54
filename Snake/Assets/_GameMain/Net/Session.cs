using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UniRx.Async;
using UniRx;
using UnityEngine;
using System.Threading;

namespace GameMain.Net
{
	public interface IMessage
	{
	}

	public interface IRequest : IMessage
	{
		int RpcId { get; set; }
	}

	public interface IResponse : IMessage
	{
		int Error { get; set; }
		string Message { get; set; }
		int RpcId { get; set; }
	}

	public class ResponseMessage : IResponse
	{
		public int Error { get; set; }
		public string Message { get; set; }
		public int RpcId { get; set; }
	}

	public class Session : IDisposable
	{
		private static int RpcId { get; set; }

		private readonly Dictionary<int, Action<IResponse>> requestCallback = new Dictionary<int, Action<IResponse>>();
		private readonly byte[] opcodeBytes = new byte[2];

		public void Dispose()
		{
			foreach (Action<IResponse> action in this.requestCallback.Values)
			{
				action.Invoke(new ResponseMessage { Error = -1 });
			}
			this.requestCallback.Clear();
		}

		private void Run(MemoryStream memoryStream)
		{

		}

		public UniTask<IResponse> Call(IRequest request)
		{
			int rpcId = ++RpcId;

			var res = GetResponseAsync(rpcId);

			request.RpcId = rpcId;
			this.Send(request);
			return res;
		}

		public UniTask<IResponse> Call(IRequest request, CancellationToken cancellationToken)
		{
			int rpcId = ++RpcId;

			var res = GetResponseAsync(rpcId,cancellationToken);

			request.RpcId = rpcId;
			this.Send(request);
			return res;
		}

		public async UniTask<IResponse> GetResponseAsync(int rpcId)
		{
			bool test = false;
			IResponse rsp = default;

			this.requestCallback[rpcId] = (response) =>
			{
				try
				{
					rsp = response;
					test = true;
				}
				catch (Exception e)
				{

				}
			};
			await UniTask.WaitUntil(() => test == true);
			return rsp;
		}

		public async UniTask<IResponse> GetResponseAsync(int rpcId, CancellationToken cancellationToken)
		{
			bool test = false;
			IResponse rsp = default;

			this.requestCallback[rpcId] = (response) =>
			{
				try
				{
					rsp = response;
					test = true;
				}
				catch (Exception e)
				{

				}
			};

			cancellationToken.Register(() => this.requestCallback.Remove(rpcId));

			await UniTask.WaitUntil(() => test == true);
			return rsp;
		}

		public void Send(IMessage message)
		{
			//TODO
		}
	}
}
