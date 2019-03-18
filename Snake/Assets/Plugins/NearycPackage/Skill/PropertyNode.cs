#region Author & Version
//==================================================== 
// Author：Nearyc 
// File name：......
// Version：V1.0.1
// Date : 2018.10.1
//*Function:
//===================================================
// Fix:
//===================================================

#endregion
using System;
namespace Nearyc.Skill
{
	[System.Serializable]
	public class PropertyNode<T> where T : struct, IComparable
	{
		public object source;
		public T value;
		public uint Id;

		public PropertyNode(T value, uint id=0, object source=null)
		{
			this.source = source;
			this.value = value;
			this.Id = id;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;
			var temp = obj as PropertyNode<T>;
			if (temp == null)
				return false;

			if (temp.Id == this.Id)
				return true;
			if (this.source == temp.source && value.CompareTo(temp.value) == 0)
				return true;
			return false;
		}
		public override int GetHashCode()
		{
			return source.GetHashCode() * 13 + value.GetHashCode();
		}
	}
}
