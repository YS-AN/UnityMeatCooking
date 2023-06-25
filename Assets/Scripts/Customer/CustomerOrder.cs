using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustOrderInfo
{
	private FoodData _orderFood;
	public FoodData OrderFood { get { return _orderFood; } }
}

public class CustomerOrder : CustomerState
{
	private const string UI_PATH = "UI/Ordering";

	private FoodData _orderFood;
	public FoodData OrderFood { get { return _orderFood; } }

	protected override void Awake()
	{
		base.Awake();
	}

	protected override void StateAction(Customer cust, CustStateType type)
	{
		base.StateAction(cust, type);

		SetOrderInfo();

		OrderingUI orderingUI = GameManager.UI.ShowInGameUI<OrderingUI>(UI_PATH);
		orderingUI.SetFoodIcon(_orderFood.Icon);
		orderingUI.SetTarget(transform);

		WaitTime = cust.Wait.WaitTime * 2;
		orderingUI.StartWait(cust, WaitTime);
	}

	private void SetOrderInfo()
	{
		int foodCnt = FoodManager.GetInstance().CanCookIndex.Count;
		int orderingNum = Random.Range(0, foodCnt);
		_orderFood = FoodManager.GetInstance().CanCookDic[orderingNum];
	}


	public override void NextAction()
	{
		if (curCustomer.CurState == CustStateType.Order)
		{
		}
	}

	public override void ClearAction()
	{
	}
}
