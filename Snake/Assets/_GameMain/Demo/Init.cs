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

		await wxb.hotMgr.Init();

		isInited = true;
	}

}
