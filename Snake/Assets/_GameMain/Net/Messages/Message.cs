using UnityEngine;

namespace GameMain.Net
{
	[Message(EOpcode.C2S_Login)]
	public class C2R_Login : Request<C2S_LoginData>
	{

	}

	[Message(EOpcode.S2C_Login)]
	public class R2C_Login : Response<S2C_LoginData>
	{

	}

	[Message(EOpcode.SomeTestMessage)]
	public class SomeMsg : Message<SomeMessageData>
	{

	}

	[MessageHandler]
	public class SomeMsgHandler : HandlerBase<SomeMsg>
	{
		protected override void Run(SomeMsg message)
		{
			Debug.Log(message.data.msg);
		}
	}

}
