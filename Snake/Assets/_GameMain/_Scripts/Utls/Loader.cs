#region Author & Version
/*******************************************************************
 ** 文件名:     
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

namespace GameMain
{
	using UnityEngine;
	using UnityEngine.SceneManagement;
	using System.Collections.Generic;

#if Use_Addressable
	using UnityEngine.AddressableAssets;
	using UnityEngine.ResourceManagement.AsyncOperations;
	using UnityEngine.ResourceManagement.ResourceLocations;
#endif

	[System.Serializable]
	public class PathRefer : AssetReference { }

	public class Loader
	{
		public const string ResourcePath = "";
		//public const string AddressablePath = "Assets/";

		public static T Load<T>(string key) where T : Object
		{
			return Resources.Load<T>(key);
		}

		public static void LoadScene(int sceneBuildIndex)
		{
			SceneManager.LoadScene(sceneBuildIndex);
		}

		public static void LoadScene(string sceneName)
		{
			SceneManager.LoadScene(sceneName);
		}

		//public static T LoadExcelDataFromResource<T>() where T : ScriptableObject
		//{
		//	Debug.Log($"{ResourcePath}Excel/{typeof(T).Name}");
		//	var req = Resources.Load<T>($"{ResourcePath}Excel/{typeof(T).Name}");
		//	return req;
		//}
#if Use_Addressable

		//public static IAsyncOperation<T> LoadExcelDataAsync<T>() where T : ScriptableObject
		//{
		//	return Addressables.LoadAsset<T>($"{AddressablePath}_Excel/Excel/{typeof(T).Name}.asset");
		//}

		public static IAsyncOperation<T> LoadAssetAsync<T>(object key) where T : class
		{
			//if (key is string)
			//	return Addressables.LoadAsset<T>($"{AddressablePath}{key}");
			//else
			return Addressables.LoadAsset<T>(key);
		}

		public static IAsyncOperation<T> LoadAssetAsync<T>(IResourceLocation location) where T : class
		{
			return Addressables.LoadAsset<T>(location);
		}

		public static IAsyncOperation<Scene> LoadScene(object key, LoadSceneMode loadMode = LoadSceneMode.Single)
		{
			//if (key is string)
			//	return Addressables.LoadScene($"{AddressablePath}{key}");
			//else
			return Addressables.LoadScene(key);
		}

		public static IAsyncOperation<Scene> LoadScene(IResourceLocation location, LoadSceneMode loadMode = LoadSceneMode.Single)
		{
			return Addressables.LoadScene(location, loadMode);
		}

		public static IAsyncOperation<GameObject> InstantiatePrefab(object key, Transform parent = null, bool isWorldPos = false)
		{
			//if (key is string)
			//	return Addressables.Instantiate<GameObject>($"{AddressablePath}_Prefabs/{key}.prefab", parent, isWorldPos);
			//else
			return Addressables.Instantiate(key, parent, isWorldPos);
		}

		public static IAsyncOperation<GameObject> InstantiatePrefab(object key, Vector3 position, Quaternion rotation, Transform parent = null)
		{
			//if (key is string)
			//	return Addressables.Instantiate<GameObject>($"{AddressablePath}_Prefabs/{key}.prefab", position, rotation,parent);
			//else
			return Addressables.Instantiate(key, position, rotation, parent);
		}

		public static IAsyncOperation<IList<GameObject>> InstantiateAll<T>(object key, System.Action<IAsyncOperation<GameObject>> onComp, Transform parent = null, bool isWorldPos = false) where T : UnityEngine.Object
		{
			//if (key is string)
			//	return Addressables.InstantiateAll<T>($"{AddressablePath}{key}", onComp, parent, isWorldPos);
			//else
			return Addressables.InstantiateAll(key, onComp, parent, isWorldPos);
		}

		public static void Release(GameObject t, float delay = 0)
		{
			Addressables.ReleaseInstance(t, delay);
		}
#else
		public static ResourceRequest LoadAssetAsync<T>(string key) where T : Object
		{
			return Resources.LoadAsync<T>($"{ResourcePath}{key}");
		}

		//public static ResourceRequest LoadExcelAsync<T>() where T : ScriptableObject
		//{
		//	return Resources.LoadAsync<T>($"{ResourcePath}Excel/{typeof(T).Name}");
		//}

		public static AsyncOperation LoadSceneAsync(int sceneBuildIndex)
		{
			return SceneManager.LoadSceneAsync(sceneBuildIndex);
		}

		public static AsyncOperation LoadSceneAsync(string sceneName)
		{
			return SceneManager.LoadSceneAsync(sceneName);
		}

		public static void Release(Object t, float delay = 0) 
		{
			GameObject.Destroy(t, delay);
		}

		public static T Instantiate<T>(T original, Transform parent, bool worldPositionStays) where T : Object
		{
			return GameObject.Instantiate<T>(original, parent, worldPositionStays);
		}

		public static T Instantiate<T>(T original, Transform parent) where T : Object
		{
			return GameObject.Instantiate<T>(original, parent);
		}

		public static T Instantiate<T>(T original, Vector3 position, Quaternion rotation, Transform parent) where T : Object
		{
			return GameObject.Instantiate<T>(original, position, rotation, parent);
		}

		public static T Instantiate<T>(T original, Vector3 position, Quaternion rotation) where T : Object
		{
			return GameObject.Instantiate(original, position, rotation);
		}

		public static T Instantiate<T>(T original) where T : Object
		{
			return GameObject.Instantiate(original);
		}
#endif
	}
}
