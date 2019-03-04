namespace GameMain.ExcelData
{
	using Nearyc.Roslyn;
	[System.Serializable]
	/// <summary>砖块的数据</summary>

	public class BlockData: IConfig
	{
		/// <summary>id</summary>
		public int index;

		/// <summary>名称</summary>
		public string Name;

		/// <summary>生命</summary>
		public int Life;

		/// <summary>攻击方式</summary>
		public EBlockAttack AttackType;

		public override string ToString()
		{
			return $" ,index:{this.index} ,Name:{this.Name} ,Life:{this.Life} ,AttackType:{this.AttackType} ";
		}
	}
}
