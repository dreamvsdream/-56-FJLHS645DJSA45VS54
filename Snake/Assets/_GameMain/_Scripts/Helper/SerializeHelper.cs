using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Bson;
//using LitJson;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

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
		return BsonSerializer.Deserialize<T>(value);
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
}
