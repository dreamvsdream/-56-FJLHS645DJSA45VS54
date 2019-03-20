using System.Threading;
using UnityEngine;
using UniRx;
using UniRx.Async;

namespace GameMain.Net
{
	public class SomeWhereTest : MonoBehaviour
	{
		private void Start()
		{
			SomeFunc();
		}

		public async void SomeFunc()
		{
			Debug.Log("Send");
			var data = new C2S_LoginData { account = "123", passwd = "456" };

			var r2cLogin = (R2C_Login)await (Client.Instance.Call(new C2R_Login { data = data }).Timeout(System.TimeSpan.FromSeconds(1000)));

			Debug.Log(r2cLogin.data.isPassed);
			Debug.Log(r2cLogin.data.msg);
			Debug.Log(r2cLogin.data.position);
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				Client.Instance.Test();
			}
		}
	}
}
