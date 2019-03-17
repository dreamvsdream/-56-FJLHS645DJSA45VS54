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
	using UnityEngine.UI;
	using System.Collections.Generic;

	public interface ILocalization
	{
		string Key { get; set; }
		string[] Value { get; set; }
		void Localize();
	}

	[RequireComponent(typeof(Text))]
	public class LocalizationText : UIElement, ILocalization
	{
		public string _key;
		public string Key
		{
			get => _key;
			set
			{
				_key = value;
				Localize();
			}
		}

		public string[] _value;
		public string[] Value
		{
			get => _value;
			set
			{
				_value = value;
				Localize();
			}
		}

		public Text Text { get; private set; }

		private void Awake()
		{
			Text = this.GetComponent<Text>();
			//this.Localize();
		}

		public void Localize()
		{
			var temp = LocalizationMgr.Instance.GetLocalizedString(Key);
			if (Text != null)
			{
				Text.text = string.Format(temp, Value);
			}
		}
	}
}
