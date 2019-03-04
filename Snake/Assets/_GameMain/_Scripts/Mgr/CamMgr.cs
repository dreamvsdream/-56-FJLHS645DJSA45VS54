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
	using Unity.Linq;

#if Use_Addressable
	using UnityEngine.AddressableAssets;
#endif
	using GameMain.Presenter;

	public class CamMgr : MonoSingleton<CamMgr>
	{
		/// <summary> 主摄像机 </summary>
		public Camera mainCam;

		/// <summary> UI摄像机 </summary>
		public Camera uiCam;

		public float targetOrth = 10;

		//public AssetReference refer;

		protected override void Awake()
		{
			base.Awake();
			mainCam = mainCam.GetComponentFromChildren(this, nameof(mainCam));
			uiCam = uiCam.GetComponentFromChildren(this, nameof(uiCam));
			//	new HeroDefineReader();
		}
		protected IEnumerator Start()
		{
			yield return null;
		}
		private void Test()
		{
			//var aop = Loader.InstantiatePrefab("Hero");
			//aop.Completed += x =>
			//{
			//	Debug.Log(x.Result.name);
			//};
		}
	}
}
