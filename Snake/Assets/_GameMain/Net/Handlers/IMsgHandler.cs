using System;
using UnityEngine;

namespace GameMain.Net
{
	public interface IMsgHandler
	{
		void Handle(object message);
		Type GetMessageType();
	}

	public abstract class HandlerBase<TMsg> : IMsgHandler where TMsg :  IMessage
	{
		protected abstract void Run(TMsg message);

		public void Handle(object msg)
		{
			TMsg message = (TMsg)msg;

			if (message == null)
			{
				Debug.LogError($"消息类型转换错误: {msg.GetType().Name} to {typeof(TMsg).Name}");
				return;
			}

			this.Run(message);
		}

		public Type GetMessageType()
		{
			return typeof(TMsg);
		}
	}
}
