using UniRx.Async;
using UnityEngine;

public class Player : MonoBehaviour
{

	private async void Start()
	{
		await UniTask.WaitUntil(() => Init.ins.isInited == true);

		TestOne();
	}

	public void TestOne()
	{
		Debug.Log("Test One");
	}


}
