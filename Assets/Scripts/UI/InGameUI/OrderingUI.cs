using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OrderingUI : InGameUI
{
	private const string NM_FOOD_BTN = "BtnFood";
	private const string NM_WAITBAR = "WaitBar";
	private const string NM_WAIT_LAYER = "Waiting";

	[SerializeField]
	private RectTransform FxHolder;

	[SerializeField]
	private Image CircleImg;

	[SerializeField]
	[Range(0, 1)]
	private float progress;

	private Customer customer;

	private OrderInfo orderData;

	private CookListUI cookListUI;

	/// <summary>
	/// 주문 가능 여부 (플레이어가 trigger 안에 들어 왔는지 확인)
	/// </summary>
	public bool IsOrderable { get; set; }

	public UnityAction OnTakeOrder;

	protected override void Awake()
	{
		base.Awake();

		IsOrderable = false;
		buttons[NM_FOOD_BTN].onClick.AddListener(() => { CheckOrder(); });
	}

	public void SetFood(OrderInfo orderData)
	{ 
		this.orderData = orderData;
		buttons[NM_FOOD_BTN].image.sprite = orderData.FoodInfo.Icon;
	}

	private void StartWaitBar(int waitMax, int waitTime)
	{
		StartCoroutine(FillSliderRoutine(waitMax, waitTime));
	}

	IEnumerator FillSliderRoutine(int waitMax, int waitTime)
	{
		int digitCount = waitMax.ToString().Length;
		float elapsedTime = 0;
		progress = 0;
		CircleImg.fillAmount = 0;

		while (elapsedTime < waitMax)
		{
			float increment = 1f / waitMax;
			progress += increment;

			elapsedTime += 1;//Time.deltaTime;
			
			progress = Mathf.Clamp(progress, 0, 1); // currentValue가 targetValue를 넘어가지 않도록 클램핑

			CircleImg.fillAmount = progress;
			FxHolder.rotation = Quaternion.Euler(new Vector3(0, 0, -progress * 360));
			yield return new WaitForSeconds(waitTime);
		}
		customer.Mover.OnExit?.Invoke();
	}

	public void StartWait(Customer curCust, int waitMax, int waitTime = 1)
	{
		customer = curCust;

		//if (images[NM_WAIT_LAYER].IsActive() == false)
		//{
		//	images[NM_WAIT_LAYER].gameObject.SetActive(true);
		//}

		StartWaitBar(waitMax, waitTime);
	}

	public void StopWait()
	{
		//waitBar.StopSlider();
	}

	public void CheckOrder()
	{
		if(IsOrderable)
		{
			if (orderData.IsOrder == false) //중복 접수 방지
			{
				orderData.IsOrder = true;
				FoodManager.GetInstance().AddOrder(orderData);

				OnTakeOrder?.Invoke();
			}
		}
	}
}
