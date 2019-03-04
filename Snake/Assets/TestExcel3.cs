using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameMain.ExcelData;

public class TestExcel3 : MonoBehaviour
{
	public TextAsset text;
    // Start is called before the first frame update
    void Start()
    {
		var temp = SerializeHelper.ArrFromJson<SkillData>(text.text);
		foreach(var i in temp)
		{
			Debug.Log(i);
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
