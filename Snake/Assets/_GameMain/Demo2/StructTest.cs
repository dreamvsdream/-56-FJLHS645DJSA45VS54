using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		int[] arr = { 1, 2, 3, 4, 5 };
		var temp = new TestStruct
		{
			pInt = -1,
			pString = "str",
			//pIntArr = arr
		};

		var by = SerializeHelper.StructToBytes(temp);
		Debug.Log(by);
		var b2s = SerializeHelper.BytesToStruct<TestStruct>(by);
		Debug.Log(b2s.pInt);
		Debug.Log(b2s.pString);
		//Debug.Log(b2s.pIntArr[2]);
    }
}
public struct TestStruct
{
	public int pInt;
	public string pString;
	//public int[] pIntArr;
}
