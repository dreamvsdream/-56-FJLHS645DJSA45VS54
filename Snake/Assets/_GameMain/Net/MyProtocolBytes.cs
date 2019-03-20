using System;
using System.Linq;
using UnityEngine;

namespace GameMain.Net
{
	///// <summary>
	///// 可增加/获取 Bytes 
	///// </summary>
	//public class MyProtocolBytes : ProtocolBytes
	//{
	//	public void AddBytes(byte[] data)
	//	{
	//		this.bytes = bytes.Concat(data).ToArray();
	//	}

	//	public byte[] GetBytes(int start)
	//	{

	//		byte[] temp = new byte[bytes.Length - start];
	//		Debug.Log(temp.Length);
	//		for (int index = start; index < bytes.Length; index++)
	//		{
	//			temp[index - start] = this.bytes[index];
	//		}
			
	//		return temp;
	//	}
	//}
}