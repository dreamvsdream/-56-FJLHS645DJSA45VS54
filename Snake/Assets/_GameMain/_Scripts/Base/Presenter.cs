#region Author & Version
/*******************************************************************
 ** 文件名:     
 ** 版  权:    (C) 深圳冰川网络技术有限公司 
 ** 创建人:     曾尔捷
 ** 日  期:    
 ** 版  本:    1.0
 ** 描  述:    
 ** 应  用:    
 **
 **************************** 修改记录 ******************************
 ** 修改人:    
 ** 日  期:    
 ** 描  述:    
 ********************************************************************/

#endregion
namespace GameMain.Presenter
{
	using UnityEngine;

	/// <summary>
	/// MVP中的P
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class Presenter<T> :MonoBehaviour where T : class
	{
		//protected static int uSID;
		//protected static T[] Arr = new T[4];
		//public T GetModel(int uID)
		//{
		//	if (uID < 0 || uID > Arr.Length)
		//		return default(T);
		//	return Presenter<T>.Arr[uID];
		//}
		//public int uID;
		//protected virtual void Awake()
		//{
		//	uID = ++Presenter<T>.uSID;
		//	if (uID > Arr.Length - 1)
		//	{
		//		var temp = Arr;
		//		Arr = new T[Arr.Length * 2];
		//		temp.CopyTo(Arr, 0);
		//	}
		//}
	}
}
