namespace GameMain.ExcelData
{

	[System.Serializable]
	/// <summary>玩家的数据</summary>

	public class PlayerData: IConfig
	{
		/// <summary>id</summary>
		public int index;

		/// <summary>注册日期</summary>
		public long RegisterTime;

		/// <summary>最近一次登录日期</summary>
		public long LastLoginTime;

		/// <summary>最高关卡数</summary>
		public int MaxLevel;

		/// <summary>是否是VIP</summary>
		public bool IsVip;

		public override string ToString()
		{
			return $" ,index:{this.index} ,RegisterTime:{this.RegisterTime} ,LastLoginTime:{this.LastLoginTime} ,MaxLevel:{this.MaxLevel} ,IsVip:{this.IsVip} ";
		}
	}
}
