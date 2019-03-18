using UnityEngine;
using UniRx.Async;
using UnityEngine.Networking;
using System;

namespace GameMain
{
	public class Test:MonoBehaviour
	{
		private void Start()
		{
			Load();
		}
		private async void Load()
		{
			//var www = new UnityWebRequest("http://172.16.20.63:59944");
			var www = new UnityWebRequest("http://127.0.0.1");
			await www.SendWebRequest();
			Debug.Log("begin");
			var aop = Loader.InstantiatePrefab("TestPrefab");
			await aop;
		}
	}
}

