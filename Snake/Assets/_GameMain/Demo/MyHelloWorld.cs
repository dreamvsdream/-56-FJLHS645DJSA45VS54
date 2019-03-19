using UnityEngine;
using UniRx.Async;

public class MyHelloWorld : MonoBehaviour
{
	private string str = "this is gooood";
	private async void Start()
	{
		await UniTask.WaitUntil(() => Init.ins.isInited == true);

		TestOne();
		TestTwo();
	}

	public void TestOne()
	{
		Debug.Log(str);
		Debug.Log(" MyHelloWorld Test One");
	}

	public void TestTwo()
	{
		Debug.Log(str);
		Debug.Log("MyHelloWorld Test Two");
	}

}
