using System.Collections.Generic;
using UnityEngine;
using UniRx.Async;
using wxb;

public class Init : MonoBehaviour
{
	public static Init ins;
	public bool isInited = false;

	private async void Awake()
	{
		ins = this;

#if USE_HOT
		var temp= UnityEngine.AddressableAssets.Addressables.GetDownloadSize("DyncDll.dll");
		//var temp2= UnityEngine.AddressableAssets.Addressables.GetDownloadSize("DyncDll.pdb");
		var temp2= UnityEngine.AddressableAssets.Addressables.GetDownloadSize("Cube");
		await temp;
		await temp2;
		Debug.Log($"Addressables.GetDownloadSize : dll {temp.Result}  - pdb {temp2.Result}");
		await wxb.hotMgr.Init();
#endif
		isInited = true;
	}

}
