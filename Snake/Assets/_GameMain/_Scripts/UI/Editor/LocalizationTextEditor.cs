﻿#region Author & Version
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

namespace GameMain.Localization
{
	using UnityEditor;

	[CustomEditor(typeof(LocalizationText))]
	public class LocalizationTextEditor : Editor
	{
		void OnEnable()
		{
			var t = target as LocalizationText;
			
		}
	}
}
