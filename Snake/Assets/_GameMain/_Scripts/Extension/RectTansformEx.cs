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
	using GameMain.Mgr;

	public static class RectTansformEx
	{
		/// <summary>
		/// 转换为世界Rect
		/// </summary>
		/// <param name="rectTransform"></param>
		/// <returns></returns>
		public static Rect ToWorldRect(this RectTransform rectTransform)
		{
			Vector2 sizeDelta = rectTransform.sizeDelta;
			float rectTransformWidth = sizeDelta.x * rectTransform.lossyScale.x;
			float rectTransformHeight = sizeDelta.y * rectTransform.lossyScale.y;
			UnityEngine.UI.CanvasScaler i;
			Vector3 position = rectTransform.position;
			return new Rect(position.x - rectTransformWidth / 2f, position.y - rectTransformHeight / 2f, rectTransformWidth, rectTransformHeight);
		}

		/// <summary>
		/// 转换为世界Rect
		/// </summary>
		/// <param name="rectTransform"></param>
		/// <returns></returns>
		public static Rect GetWorldRect(this RectTransform rt, float? scale=null)
		{
			if (!scale.HasValue)
			{
				scale = UIMgr.Instance.canvasScaler.scaleFactor;
			}
			// Convert the rectangle to world corners and grab the top left
			Vector3[] corners = new Vector3[4];
			rt.GetWorldCorners(corners);
			Vector3 topLeft = corners[0];

			// Rescale the size appropriately based on the current Canvas scale
			Vector2 scaledSize = new Vector2(scale.Value * rt.rect.size.x, scale.Value * rt.rect.size.y);

			return new Rect(topLeft, scaledSize);
		}

	}
}
