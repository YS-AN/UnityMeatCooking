using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WaitBar : InGameUI
{
	private const int MAX = 1;

	[SerializeField]
	private Slider slider;


	private void OnEnable()
	{
		StartCoroutine(FillWaitBarRoutine());
	}

	IEnumerator FillWaitBarRoutine()
	{
		while (slider.value >= MAX)
		{
			slider.value += 0.1f;
			yield return null;
		}
	}
}
