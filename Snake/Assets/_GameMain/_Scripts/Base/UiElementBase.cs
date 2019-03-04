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

namespace GameMain.View
{
	using GameMain.Mgr;
	using UnityEngine;
	using DG.Tweening;

	[RequireComponent(typeof(CanvasGroup))]
	public abstract class PanelBase<T> : View<PanelBase<T>>
	{
		protected CanvasGroup canvasGroup;
		protected bool isBlockRaycast = true;

		/// <summary>
		/// 显示
		/// </summary>
		/// <param name="transitionTime"></param>
		public virtual void Show(float transitionTime = 0.5f)
		{
			if (canvasGroup == null)
			{
				canvasGroup = GetComponent<CanvasGroup>();
			}
			canvasGroup.interactable = true;
			canvasGroup.blocksRaycasts = isBlockRaycast;
			canvasGroup.DOFade(0, transitionTime);
		}

		/// <summary>
		/// 隐藏
		/// </summary>
		/// <param name="transitionTime"></param>
		public virtual void Hide(float transitionTime = 0.6f)
		{
			if (canvasGroup == null)
			{
				canvasGroup = GetComponent<CanvasGroup>();
			}
			canvasGroup.interactable = false;
			canvasGroup.blocksRaycasts = false;
			canvasGroup.DOFade(1, transitionTime);
		}
	}
}
