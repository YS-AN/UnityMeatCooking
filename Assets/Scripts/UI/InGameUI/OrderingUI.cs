using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class OrderingUI : InGameUI
{
	private const string NM_FOOD_BTN = "BtnFood";
	private const string NM_WAITBAR = "WaitBar";
	private const string NM_WAIT_LAYER = "Waiting";

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

	public void StartWait(Customer curCust, int waitMax, int waitTime = 1)
	{
		if(images[NM_WAIT_LAYER].IsActive() == false)
		{
			images[NM_WAIT_LAYER].gameObject.SetActive(true);
		}

		WaitBar waitBar = images[NM_WAIT_LAYER].GetComponent<WaitBar>();
		waitBar.customer = curCust;
		waitBar.StartSlider(waitMax, waitTime);
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
