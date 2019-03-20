using System.Collections;
using System.Collections.Generic;
using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;

public class ShowInfo : MonoBehaviour
{
	private Text text;
	private string info="init_1";

	private void Awake()
	{
		text = this.GetComponent<Text>();
	}
	// Start is called before the first frame update
	private async void Start()
	{
		await UniTask.WaitUntil(() => GameMain.Init.ins.isInited == true);

		ShowMyInfo();
	}

	void ShowMyInfo()
	{
		text.text = info;
	}
}
