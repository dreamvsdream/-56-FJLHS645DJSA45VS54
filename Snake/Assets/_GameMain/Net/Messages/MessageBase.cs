using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UniRx.Async;
using UniRx;
using System.Threading;

namespace GameMain.Net
{
	public static class Ex
	{
		public static T RequestSetData<T, U>(this T t, U data) where T : Request<U> where U : struct
		{
			t.data = data;
			return t;
		}

		public static T ResponseSetData<T, U>(this T t, U data) where T : Response<U> where U : struct
		{
			t.data = data;
			return t;
		}

		public static T MessageSetData<T, U>(this T t, U data) where T : Message<U> where U : struct
		{
			t.data = data;
			return t;
		}
	}

	public abstract class Request<T> : IRequest where T : struct
	{
		public int RpcId { get; set; }
		public byte[] Bytes
		{
			get
			{
				return SerializeHelper.StructToBytes(data);
			}
			set
			{
				data = SerializeHelper.BytesToStruct<T>(value);
			}
		}

		public T data;


	}

	public abstract class Response : IResponse
	{
		public int Error { get; set; }
		public int RpcId { get; set; }

		public virtual byte[] Bytes { get; set; }
	}

	public class ResponseMessage : Response
	{

	}

	public abstract class Response<T> : Response where T : struct
	{
		public override byte[] Bytes
		{
			get
			{
				return SerializeHelper.StructToBytes(data);
			}
			set
			{
				data = SerializeHelper.BytesToStruct<T>(value);
			}
		}

		public T data;

	}

	public abstract class Message<T> : IMessage where T : struct
	{
		public byte[] Bytes
		{
			get
			{
				return SerializeHelper.StructToBytes(data);
			}
			set
			{
				data = SerializeHelper.BytesToStruct<T>(value);
			}
		}

		public T data;


	}
}
