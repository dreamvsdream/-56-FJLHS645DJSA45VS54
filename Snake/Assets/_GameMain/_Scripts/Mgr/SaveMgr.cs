﻿#region Author & Version
/*******************************************************************
 ** 文件名:     SliderEntity.cs
 ** 版  权:    (C) 深圳冰川网络技术有限公司 
 ** 创建人:     曾尔捷
 ** 日  期:    2018/11/1
 ** 版  本:    1.0
 ** 描  述:    调用管理
 ** 应  用:    
 **
 **************************** 修改记录 ******************************
 ** 修改人:    
 ** 日  期:    
 ** 描  述:    
 ********************************************************************/

#endregion

namespace GameMain.Mgr
{
	using System.Collections;
	using UnityEngine;

	/// <summary>
	/// 管理数据本地存储
	/// </summary>
	public class SaveMgr : MonoSingleton<SaveMgr>
	{
		private void OnApplicationQuit()
		{
			PlayerPrefs.Save();
		}

		private static string _Serialize<T>(T toSerialize) where T : class
		{
			return SerializeHelper.ToJson(toSerialize);
		}

		private static T _DeSerialize<T>(string toDeSerialize) where T : class
		{
			return SerializeHelper.FromJson<T>(toDeSerialize);
		}

		/// <summary>
		/// 存储数据，默认Key为类的Type
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="serializedClass"></param>
		/// <param name="key"></param>
		public static void SaveData<T>(T serializedClass, string key = null) where T : class
		{
			key = key ?? typeof(T).ToString();
			PlayerPrefs.SetString(key, _Serialize(serializedClass));
		}

		/// <summary>
		/// 读取数据，默认Key为类的Type
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key"></param>
		/// <returns></returns>
		public static T LoadData<T>(string key = null) where T : class, new()
		{
			key = key ?? typeof(T).ToString();
			if (PlayerPrefs.HasKey(key))
			{
				var temp = PlayerPrefs.GetString(key);
				return _DeSerialize<T>(temp) as T;
			}
			else
			{
				Debug.Log("New Data  " + typeof(T));
				return new T();
			}
		}
	}

}
