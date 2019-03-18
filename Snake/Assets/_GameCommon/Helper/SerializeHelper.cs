using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Bson;
//using LitJson;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System.Runtime.InteropServices;
using System;

public static class SerializeHelper
{
	/// <summary>
	/// 从Json反序列化
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="value"></param>
	/// <returns></returns>
	public static T FromJson<T>(string value)
	{
		if (typeof(T).IsValueType)
		{
			return JsonUtility.FromJson<T>(value);
		}
		else
		{
			return BsonSerializer.Deserialize<T>(value);
		}
	}

	/// <summary>
	/// 从Json数组反序列化
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="value"></param>
	/// <returns></returns>
	public static T[] ArrFromJson<T>(string value)
	{
		string[] arr = value.Split('\n');
		List<T> tempList = new List<T>();
		foreach (var str in arr)
		{
			if (str != "")
			{
				tempList.Add(FromJson<T>(str));
			}
		}
		return tempList.ToArray();
	}

	/// <summary>
	/// 序列化成Json
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="t"></param>
	/// <returns></returns>
	public static string ToJson<T>(T t)
	{
		return t.ToJson();
		//return JsonUtility.ToJson(t);
	}

	/// <summary>
	/// 浅拷贝
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="t"></param>
	/// <returns></returns>
	public static T Copy<T>(T t)
	{
		return FromJson<T>(ToJson(t));
	}

	/// <summary>
	/// struct转换为byte[]
	/// </summary>
	/// <param name="structObj"></param>
	/// <returns></returns>
	public static byte[] StructToBytes<T>(T structObj) where T : struct
	{
		int size = Marshal.SizeOf(structObj);
		IntPtr buffer = Marshal.AllocHGlobal(size);

		try
		{
			Marshal.StructureToPtr(structObj, buffer, false);
			byte[] bytes = new byte[size];
			Marshal.Copy(buffer, bytes, 0, size);
			return bytes;
		}
		finally
		{
			Marshal.FreeHGlobal(buffer);
		}
	}

	/// <summary>
	/// byte[]转换为struct
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="bytes"></param>
	/// <param name="strcutType"></param>
	/// <returns></returns>
	public static T BytesToStruct<T>(byte[] bytes) where T : struct
	{
		var strcutType = typeof(T);
		int size = Marshal.SizeOf(strcutType);
		IntPtr buffer = Marshal.AllocHGlobal(size);

		try
		{
			Marshal.Copy(bytes, 0, buffer, size);
			return (T)Marshal.PtrToStructure(buffer, strcutType);
		}
		finally
		{
			Marshal.FreeHGlobal(buffer);
		}
	}

	/*
	/// <summary>
	/// 序列化为Byte[]
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="obj"></param>
	/// <returns></returns>
	public static byte[] ToBytes<T>(T obj) 
	{
		if (obj == null) return null;

		using(var ms = new MemoryStream())
		{
			var bf = new BinaryFormatter();
			bf.Serialize(ms, obj);
			return ms.ToArray();
		}
	}

	/// <summary>
	/// 从Byte[]反序列化
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="buffer"></param>
	/// <returns></returns>
	public static T FromBytes<T>(byte[] buffer)
	{
		if (buffer == null) return default;

		using (var ms = new MemoryStream())
		{
			ms.Write(buffer,0,buffer.Length);
			var bf = new BinaryFormatter();
		 	return (T)bf.Deserialize(ms);
		}
	}
	 */
}
