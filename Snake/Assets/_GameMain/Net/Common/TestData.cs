using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UniRx.Async;
using UniRx;
using System.Threading;
using UnityEngine;

namespace GameMain.Net
{
	public struct C2S_LoginData
	{
		public string account;
		public string passwd;
	}

	public struct S2C_LoginData
	{
		public string msg;
		public bool isPassed;
		public Vector3 position;
	}

	public struct SomeMessageData
	{
		public string msg;
	}

}
