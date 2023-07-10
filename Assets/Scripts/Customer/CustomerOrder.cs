using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CustomerOrder : StatusController
{
	private const string UI_PATH = "UI/Ordering";
	private OrderingUI orderingUI;

	private OrderInfo _orderData;
	public OrderInfo OrderData { get { return _orderData; } }

	protected override void Awake()
	{
		base.Awake();

		orderingUI = null;
	}

	protected override void Start()
	{
		base.Start();
	}

	public override void StateAction(Customer cust, CustStateType type)
	{
		base.StateAction(cust, type);

		SetOrderInfo();

		orderingUI = GameManager.UI.ShowInGameUI<OrderingUI>(UI_PATH);
		orderingUI.SetFood(_orderData);
		orderingUI.SetTarget(transform);
		orderingUI.IsOrderable = true;
		orderingUI.OnTakeOrder += TakeOrder;

		WaitTime = cust.Wait.WaitTime * 2;
		orderingUI.StartWait(cust, WaitTime);
	}

	/// <summary>
	/// �ֹ��� �޴� ����
	/// </summary>
	private void SetOrderInfo()
	{
		int foodCnt = FoodManager.GetInstance().CanCookIndex.Count;
		int orderingNum = Random.Range(0, foodCnt);

		_orderData = new OrderInfo(FoodManager.GetInstance().CanCookDic[orderingNum]);
	}

	/// <summary>
	/// �ֹ� ���� �� �ൿ
	/// </summary>
	private void TakeOrder()
	{
		curCustomer.Eater.OnStateAction?.Invoke(curCustomer, CustStateType.Eating);
	}

	public override void NextAction()
	{
		if(orderingUI != null) 
			orderingUI.IsOrderable = true;
	}

	public override void ClearAction()
	{
		if (orderingUI != null)
			orderingUI.IsOrderable = false;
	}

	public override void TakeActionAfterNoti()
	{
		if(orderingUI != null)
		{
			CloseUI();
			RemoveOrder();
		}
	}

	/// <summary>
	/// �ֹ� �޴� â ����
	/// </summary>
	public void CloseUI()
	{
		if(orderingUI != null)
		{
			GameManager.UI.CloseInGameUI(orderingUI);
			orderingUI = null;
		}
	}

	/// <summary>
	/// �ֹ� �޴� ����
	/// </summary>
	private void RemoveOrder()
	{
		if (OrderData != null && OrderData.IsOrder && curCustomer.CurState == CustStateType.Order) 
		{
			var foodInfo = FoodManager.GetInstance().GetOrderList().LastOrDefault(x => x.Value.FoodInfo.Name == OrderData.FoodInfo.Name); //�����ϴ� ���� �ֹ��� ���İ� ������ ������ ���� ������ ������ ����

			if (!foodInfo.Equals(default))
			{
				FoodManager.GetInstance().RemoveOrder(foodInfo.Key);
			}
		}
	}

}
