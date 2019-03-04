using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Linq;

public class SnakeV : MonoBehaviour
{
	public Slider healthSlider;
	public Button skillOne;
	public Button skillTwo;

	private void Awake()
	{
		GetUI();
	}

	/// <summary>
	/// 得到所有UI引用
	/// </summary>
	private void GetUI()
	{
		//...
		healthSlider = healthSlider.GetComponentFromDescendants(this, nameof(healthSlider));
		skillOne = skillOne.GetComponentFromDescendants(this, nameof(skillOne));
		skillTwo = skillTwo.GetComponentFromDescendants(this, nameof(skillTwo));
	}
   
}
