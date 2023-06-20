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
		//slider.interactable = (transform.position.z >= 0); //z�� ��ü�� ī�޶���� �Ÿ��� ��Ÿ��. 0���� ������ ī�޶� �ڿ� �ִٴ� �ǹ���

		//z�� ��ü�� ī�޶���� �Ÿ��� ��Ÿ��. 0���� ������ ī�޶� �ڿ� �ִٴ� �ǹ���
		//0���� ������ ��Ȱ��ȭ�� ������� �ϴµ�..... ��� �ϴ��� �𸣰���.
		//SetActive(false)�ϸ� ���Ŀ� true�� �ٲ��൵ �����̴��� �ٽ� �� ����;;;
		//GetComponent<Slider>().gameObject.SetActive(transform.position.z >= 0); => ���� 

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
