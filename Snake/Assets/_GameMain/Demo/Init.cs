using System.Collections.Generic;
using UnityEngine;
using wxb;
using GameMain;
using UniRx.Async;

namespace GameMain
{

	public class Init : MonoBehaviour
	{
		public static Init ins;
		public bool isInited = false;

		private async void Awake()
		{
			ins = this;
#if USE_HOT
			//var temp2= UnityEngine.AddressableAssets.Addressables.GetDownloadSize("DyncDll.pdb");
			var a = UnityEngine.AddressableAssets.Addressables.GetDownloadSize("DyncDll.dll").ToMyUnitask();
			var b = UnityEngine.AddressableAssets.Addressables.GetDownloadSize("Cube").ToMyUnitask();
			var (r1, r2) = await UniTask.WhenAll(a, b);
			Debug.Log($"Addressables.GetDownloadSize : dll {r1}  - pdb {r2}");
			await wxb.hotMgr.Init();
#endif
			isInited = true;
		}

	}
}