using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WaitBar : InGameUI
{
	private Slider slider;
	private Coroutine fillSliderRoutine; 

	public Customer customer;

	bool a = true;
	private void Awake()
	{
		slider = GetComponent<Slider>();
	}

	private void Update()
	{
		//slider.interactable = (transform.position.z >= 0); //z는 물체와 카메라까지 거리를 나타냄. 0보다 작으면 카메라 뒤에 있다는 의미임

		//z는 물체와 카메라까지 거리를 나타냄. 0보다 작으면 카메라 뒤에 있다는 의미임
		//0보다 작으면 비활성화를 시켜줘야 하는데..... 어떻게 하는지 모르겠음.
		//SetActive(false)하면 이후에 true로 바꿔줘도 슬라이더가 다시 안 보임;;;
		//GetComponent<Slider>().gameObject.SetActive(transform.position.z >= 0); => 망함 

	}

	public void StartSlider(int max, int waitTime = 1)
	{
		SetSliderValue(max);
		fillSliderRoutine = StartCoroutine(FillSliderRoutine(max, waitTime));
	}

	IEnumerator FillSliderRoutine(int max, int waitTime)
	{
		while (slider.value < max)
		{
			slider.value += 1;
			yield return new WaitForSeconds(waitTime);
		}
		Destroy(gameObject);
	}

	public void StopSlider()
	{
		if(fillSliderRoutine != null)
		{
			StopCoroutine(fillSliderRoutine);
			Destroy(gameObject);
		}
	}

	private void SetSliderValue(int max)
	{
		slider.value = 0;

		slider.minValue = 0;
		slider.maxValue = max;
	}
}
