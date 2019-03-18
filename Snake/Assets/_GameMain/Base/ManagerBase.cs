#region Author & Version
/*******************************************************************
 ** 文件名:     
 ** 版  权:    (C) 深圳冰川网络技术有限公司 
 ** 创建人:     曾尔捷
 ** 日  期:    
 ** 版  本:    1.0
 ** 描  述:    
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
	using UnityEngine;

	/// <summary>
	/// 普通单例类
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class Singleton<T> where T: Singleton<T>,new()
	{
		public static  T _instance;
		public static T Instance
		{
			get
			{
				if (_instance==null)
				{
					_instance = new T();
				}
				return _instance;
			}
		}
	}

	/// <summary>
	/// Mono单例类
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class MonoSingleton<T> :MonoBehaviour where T: MonoSingleton<T>,new()
	{
		private static T _instance;

		private static object _lock = new object();

		private static bool applicationIsQuitting = false;

		public static T Instance
		{
			get
			{
				if (applicationIsQuitting)
				{
					return null;
				}

				lock (_lock)
				{
					if (_instance == null)
					{
						_instance = (T)FindObjectOfType(typeof(T));

						if (FindObjectsOfType(typeof(T)).Length > 1)
						{
							return _instance;
						}

						if (_instance == null)
						{
							GameObject singleton = new GameObject();
							_instance = singleton.AddComponent<T>();
							singleton.name = "[singleton] " + typeof(T).ToString();

							DontDestroyOnLoad(singleton);
						}
					}

					return _instance;
				}
			}
		}

		protected virtual void Awake()
		{
			if(_instance != null)
			{
				Destroy(this);
				return;
			}
			_instance = this as T;
		}

		public void OnDestroy()
		{
			applicationIsQuitting = true;
		}
	}
}
