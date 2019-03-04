namespace GameMain.ExcelData
{

	[System.Serializable]
	/// <summary>技能的数据</summary>

	public class SkillData: IConfig
	{
		/// <summary>id</summary>
		public int index;

		/// <summary>名称</summary>
		public string Name;

		/// <summary>是否为持续效果</summary>
		public bool IsDurationEffect;

		/// <summary>蛇头威力增加</summary>
		public int HeadAttackGain;

		/// <summary>子弹威力增加</summary>
		public int BulletAttackGain;

		/// <summary>持续时间</summary>
		public float DurationTime;

		/// <summary>特殊效果</summary>
		public EPropSpecialEffect SpecialEffect;

		public override string ToString()
		{
			return $" ,index:{this.index} ,Name:{this.Name} ,IsDurationEffect:{this.IsDurationEffect} ,HeadAttackGain:{this.HeadAttackGain} ,BulletAttackGain:{this.BulletAttackGain} ,DurationTime:{this.DurationTime} ,SpecialEffect:{this.SpecialEffect} ";
		}
	}
}
