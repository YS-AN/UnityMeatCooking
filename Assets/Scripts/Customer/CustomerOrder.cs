using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustOrderInfo
{
	private FoodData _orderFood;
	public FoodData OrderFood { get { return _orderFood; } }
}

public class CustomerOrder : MonoBehaviour
{
	private const string UI_PATH = "UI/Ordering";

	private FoodData _orderFood;
	public FoodData OrderFood { get { return _orderFood; } }

	public Customer curCustomer;

	public int WaitTime;

	public UnityAction<Customer> OnOrder;

	private void Awake()
	{
		OnOrder += Order;
	}

	
	public void Order(Customer curCust)
	{
		curCustomer = curCust;

		SetOrderInfo();

		OrderingUI orderingUI = GameManager.UI.ShowInGameUI<OrderingUI>(UI_PATH);
		orderingUI.SetFoodIcon(_orderFood.Icon);
		orderingUI.SetTarget(transform);

		WaitTime = curCust.Wait.WaitTime * 2;
		orderingUI.StartWait(curCust, WaitTime);
	}

	private void SetOrderInfo()
	{
		int foodCnt = FoodManager.GetInstance().CanCookIndex.Count;
		int orderingNum = Random.Range(0, foodCnt);
		_orderFood = FoodManager.GetInstance().CanCookDic[orderingNum];
	}

}
