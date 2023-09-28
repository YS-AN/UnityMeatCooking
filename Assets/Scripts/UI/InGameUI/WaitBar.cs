using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WaitBar : InGameUI
{
	private const string NM_WAITBAR = "WaitBar";

	private Slider waitSlider;
	private Coroutine fillSliderRoutine; 

	public Customer customer;


	protected override void Awake()
	{
		base.Awake();
	}

	public void StartSlider(int max, int waitTime = 1)
	{
		waitSlider = sliders[NM_WAITBAR];

		SetSliderValue(max);
		fillSliderRoutine = StartCoroutine(FillSliderRoutine(max, waitTime));
	}

	IEnumerator FillSliderRoutine(int max, int waitTime)
	{
		while (sliders[NM_WAITBAR].value < max)
		{
			sliders[NM_WAITBAR].value += 1;
			yield return new WaitForSeconds(waitTime);
		}
		fillSliderRoutine = null;
		customer.Mover.OnExit?.Invoke();
	}

	public void StopSlider()
	{
		if(fillSliderRoutine != null)
		{
			StopCoroutine(fillSliderRoutine);
			GameManager.UI.CloseInGameUI(this);
		}
	}

	private void SetSliderValue(int max)
	{
		waitSlider.value = 0;

		waitSlider.minValue = 0;
		waitSlider.maxValue = max;
	}
}
