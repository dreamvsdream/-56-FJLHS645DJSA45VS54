using ILRuntime.Runtime.Intepreter;
using UnityEngine;

namespace GameHotfix
{
	public class ILBehaviourTest: ILTypeInstance
	{
		GameObject go;

		public ILBehaviourTest(GameObject go)
		{
			this.go = go;
			Start();
		}

		public void Start()
		{
			Debug.Log(go.transform.position);
		}

		public void Update()
		{
			Debug.Log(go.transform.position);
			if (go != null)
			{
				go.transform.position += Vector3.up * Time.deltaTime * 10f;
			}
		}
	}
}
