using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

//관찰자 = 구독자
public class CustomerOrder : CustomerState
{
	private const string UI_PATH = "UI/Ordering";

	private OrderInfo _orderFood;
	public OrderInfo OrderFood { get { return _orderFood; } }

	private OrderingUI orderingUI;

	protected override void Awake()
	{
		base.Awake();

		orderingUI = null;
	}

	protected override void Start()
	{
		base.Start();
	}

	protected override void StateAction(Customer cust, CustStateType type)
	{
		base.StateAction(cust, type);

		SetOrderInfo();

		orderingUI = GameManager.UI.ShowInGameUI<OrderingUI>(UI_PATH);
		orderingUI.SetFood(_orderFood);
		orderingUI.SetTarget(transform);

		WaitTime = cust.Wait.WaitTime * 2;
		orderingUI.StartWait(cust, WaitTime);
	}

	private void SetOrderInfo()
	{
		int foodCnt = FoodManager.GetInstance().CanCookIndex.Count;
		int orderingNum = Random.Range(0, foodCnt);

		_orderFood = new OrderInfo(FoodManager.GetInstance().CanCookDic[orderingNum]);
		//_orderFood.FoodInfo = FoodManager.GetInstance().CanCookDic[orderingNum];
		//_orderFood.IsOrder = false;
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

	public override void TakeActionAfterNoti()
	{
		if(orderingUI != null)
		{
			GameManager.UI.CloseInGameUI(orderingUI);
			orderingUI = null;

			RemoveOrder();
		}
	}

	private void RemoveOrder()
	{
		if (OrderFood != null && OrderFood.IsOrder)
		{
			var foodInfo = FoodManager.GetInstance().GetOrderList().LastOrDefault(x => x.Value.Name == OrderFood.FoodInfo.Name); //퇴장하는 고객이 주문한 음식과 동일한 종류의 가장 마지막 음식을 제거

			if (!foodInfo.Equals(default))
			{
				FoodManager.GetInstance().RemoveOrder(foodInfo.Key);
			}
		}
	}

}
