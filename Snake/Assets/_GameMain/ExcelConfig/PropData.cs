namespace GameMain.ExcelData
{

	[System.Serializable]
	/// <summary>道具的数据</summary>

	public class PropData: IConfig
	{
		/// <summary>id</summary>
		public int index;

		/// <summary>名称</summary>
		public string Name;

		/// <summary>是否为持续效果</summary>
		public bool IsDurationEffect;

		/// <summary>生命加成</summary>
		public int LifeGain;

		/// <summary>移动速度百分比加成</summary>
		public float MovementMore;

		/// <summary>持续时间</summary>
		public float DurationTime;

		/// <summary>特殊效果</summary>
		public EPropSpecialEffect SpecialEffect;

		public override string ToString()
		{
			return $" ,index:{this.index} ,Name:{this.Name} ,IsDurationEffect:{this.IsDurationEffect} ,LifeGain:{this.LifeGain} ,MovementMore:{this.MovementMore} ,DurationTime:{this.DurationTime} ,SpecialEffect:{this.SpecialEffect} ";
		}
	}
}
