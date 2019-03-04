using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameMain.ExcelData;
using System.Linq;

public class SnakeP : MonoBehaviour
{
	public SnakeV v;
	public SnakeData m;

	public TextAsset modelText;

	private void Start()
	{
		m = SerializeHelper.ArrFromJson<SnakeData>(modelText.text).FirstOrDefault();
		Bind(v, m);
	}

	private void Update()
	{
		BindUpdate(v, m);
	}

	private void Bind(SnakeV v, SnakeData m)
	{
		v.skillOne.onClick.AddListener(() =>
		{
			m.MovementSpeed++;
		});
	}

	private void BindUpdate(SnakeV v, SnakeData m)
	{
		v.healthSlider.value = m.Life / 100;
	}
}
