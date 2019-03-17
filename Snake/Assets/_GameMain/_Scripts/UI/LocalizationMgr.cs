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

namespace GameMain.Localization
{
	using UnityEngine;
	using GameMain.Mgr;
	using System.Collections.Generic;

	public enum ELanguage
	{
		Zh_cn = 0,
		En,
	}

	public class LocalizationMgr : Singleton<LocalizationMgr>
	{
		public ELanguage CurrentLanguage { get; private set; } = ELanguage.Zh_cn;

		Dictionary<string, string> Zh_cn = new Dictionary<string, string>();
		Dictionary<string, string> En = new Dictionary<string, string>();

		public string GetLocalizedString(string key)
		{
			if (string.IsNullOrEmpty(key) && string.IsNullOrWhiteSpace(key))
			{
				string temp;
				switch (this.CurrentLanguage)
				{
					case ELanguage.Zh_cn:
						if (Zh_cn.TryGetValue(key, out temp)) return temp;
						break;
					case ELanguage.En:
						if (En.TryGetValue(key, out temp)) return temp;
						break;
				}
			}

			Debug.LogError($"No such key : {key}");
			return "";
		}
	}
}
