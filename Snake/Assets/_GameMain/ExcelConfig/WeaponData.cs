namespace GameMain.ExcelData
{

	[System.Serializable]
	/// <summary>武器的数据</summary>

	public class WeaponData: IConfig
	{
		/// <summary>id</summary>
		public int index;

		/// <summary>名称</summary>
		public string Name;

		/// <summary>攻击力加成百分比</summary>
		public float AttackMore;

		/// <summary>攻击方式</summary>
		public EWeaponAttack AttackType;

		public override string ToString()
		{
			return $" ,index:{this.index} ,Name:{this.Name} ,AttackMore:{this.AttackMore} ,AttackType:{this.AttackType} ";
		}
	}
}
