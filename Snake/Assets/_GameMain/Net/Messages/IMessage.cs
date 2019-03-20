namespace GameMain.Net
{
	public interface IMessage
	{
		byte[] Bytes { get; set; }
	}

	public interface IRequest : IMessage
	{
		int RpcId { get; set; }
	}

	public interface IResponse : IMessage
	{
		int RpcId { get; set; }
		int Error { get; set; }
		//string Message { get; set; }
	}

}
