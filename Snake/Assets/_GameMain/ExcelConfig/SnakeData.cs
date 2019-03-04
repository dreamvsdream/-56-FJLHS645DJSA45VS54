namespace GameMain.ExcelData
{

	[System.Serializable]
	/// <summary>蛇的数据</summary>

	public class SnakeData: IConfig
	{
		/// <summary>id</summary>
		public int index;

		/// <summary>生命</summary>
		public int Life;

		/// <summary>蛇头攻击力</summary>
		public int HeadAttack;

		/// <summary>子弹攻击力</summary>
		public int BulletAttack;

		/// <summary>移动速度</summary>
		public float MovementSpeed;

		public override string ToString()
		{
			return $" ,index:{this.index} ,Life:{this.Life} ,HeadAttack:{this.HeadAttack} ,BulletAttack:{this.BulletAttack} ,MovementSpeed:{this.MovementSpeed} ";
		}
	}
}
