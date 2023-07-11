using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CustomerEater : StatusController
{
	private const string UI_PATH = "UI/Eated";

	private OrderInfo orderData;
	private Dish servedFood;

	protected override void Awake()
	{
		base.Awake();
	}

	protected override void Start()
	{
		base.Start();
	}

	public override void StateAction(Customer cust, CustStateType type)
	{
		base.StateAction(cust, type);
	}
	
	public override void NextAction()
	{
		OrderInfo holdingFood = PlayerManager.GetInstance().Player.Cooker.CurrentHoldingFood(curCustomer.Order.OrderData.OderID);

		if (holdingFood != null)
		{
			PutFoodDown();
		}
	}

	public override void ClearAction()
	{
	}

	public override void TakeActionAfterNoti()
	{
	}

	private void PutFoodDown()
	{
		orderData = curCustomer.Order.OrderData;

		int seatPntIndex = curCustomer.Mover.info.SeatPointIndex;
		var putDonwPoint = curCustomer.Mover.info.Chair.SeatPoints[seatPntIndex].GetComponentInChildren<FoodPoint>();

		servedFood = PlayerManager.GetInstance().Player.Cooker.PutDownDish(putDonwPoint.transform, orderData.OderID);
		if (servedFood != null)
			DoEatingFood();
	}

	private void DoEatingFood()
	{
		curCustomer.Order.CloseUI();

		WaitTime = Random.Range(30, 41);

		Coroutines coroutines = new Coroutines();
		StartCoroutine(coroutines.JustWaitRoutine(5, RateTaste));
	}


	private void RateTaste()
	{
		CustomerRateType rateType = CustomerRateType.None;
		switch (orderData.CookResultType)
		{
			case CookedType.Undercooked:
				rateType = CustomerRateType.Under; break;
			case CookedType.Perfect:
				rateType = CustomerRateType.Good; break;
			case CookedType.Overcooked: 
				rateType = CustomerRateType.Over; break;
		}
		curCustomer.Rater.OnRate?.Invoke(rateType);

		Coroutines coroutines = new Coroutines();
		StartCoroutine(coroutines.JustWaitRoutine(10, Payment)); 
	}

	private void Payment()
	{
		curCustomer.Rater.CloseRateView();

		GameManager.Data.Revenue += orderData.FoodInfo.Price; //µ· ÁöºÒ
		
		curCustomer.Mover.OnExit?.Invoke();
		Destroy(servedFood.gameObject);
	}
}
