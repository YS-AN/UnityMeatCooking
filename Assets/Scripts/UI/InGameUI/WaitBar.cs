using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WaitBar : InGameUI
{
	private const string NM_WAITBAR = "WaitBar";

	private Coroutine fillSliderRoutine; 

	public Customer customer;

	protected override void Awake()
	{
		base.Awake();
	}

	private void Update()
	{
		//todo (��¥ �� �߿�!!!)
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
		while (sliders[NM_WAITBAR].value < max)
		{
			sliders[NM_WAITBAR].value += 1;
			yield return new WaitForSeconds(waitTime);
		}
		Destroy(gameObject);
		customer.Mover.OnExit?.Invoke(customer);
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
		sliders[NM_WAITBAR].value = 0;

		sliders[NM_WAITBAR].minValue = 0;
		sliders[NM_WAITBAR].maxValue = max;
	}
}
