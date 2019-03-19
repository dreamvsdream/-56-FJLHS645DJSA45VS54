using System.Collections.Generic;
using UnityEngine;
using wxb;

public class Init : MonoBehaviour
{
	public static Init ins;
	public bool isInited = false;

	private async void Awake()
	{
		ins = this;
#if USE_HOT
		await wxb.hotMgr.Init();
#endif
		isInited = true;
	}

}
