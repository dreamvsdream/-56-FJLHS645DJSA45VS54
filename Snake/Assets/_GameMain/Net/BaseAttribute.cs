using System;

namespace GameMain.Net
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class BaseAttribute : Attribute
	{
		public Type AttributeType { get; }

		public BaseAttribute()
		{
			this.AttributeType = this.GetType();
		}
	}

	public class MessageAttribute : BaseAttribute
	{
		public ushort Opcode { get; }

		public MessageAttribute(ushort code)
		{
			this.Opcode = code;
		}

		public MessageAttribute(EOpcode code)
		{
			this.Opcode =(ushort) code;
		}
	}

	public class MessageHandlerAttribute : BaseAttribute
	{
		public MessageHandlerAttribute()
		{

		}
	}
}
