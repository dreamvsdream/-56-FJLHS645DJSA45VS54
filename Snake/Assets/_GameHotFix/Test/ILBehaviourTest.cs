using UnityEngine;

namespace GameHotfix
{
	public class ILBehaviourTest
	{
		GameObject go;

		public ILBehaviourTest(GameObject go)
		{
			this.go = go;
			Start();
		}

		public ILBehaviourTest()
		{

		}

		public void Start()
		{
			Debug.Log(go.transform.position);
		}

		public void Update()
		{
			if (Input.GetMouseButton(0))
			{
				Debug.Log("Test");
			}
		}
	}
}
