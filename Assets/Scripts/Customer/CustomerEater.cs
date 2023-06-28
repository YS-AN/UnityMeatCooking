using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CustomerEater : CustomerState
{
	private const string UI_PATH = "UI/Eated";

	private FoodData foodData;

	protected override void Awake()
	{
		base.Awake();
	}

	protected override void Start()
	{
		base.Start();
	}

	protected override void StateAction(Customer cust, CustStateType type)
	{
		base.StateAction(cust, type);

		PutFoodDown();
	}

	private void PutFoodDown()
	{
		int seatPntIndex = curCustomer.Mover.info.SeatPointIndex;
		var putDonwPoint = curCustomer.Mover.info.Chair.SeatPoints[seatPntIndex].GetComponentInChildren<FoodPoint>();

		foodData = curCustomer.Order.OrderFood.FoodInfo;

		if(PlayerManager.GetInstance().Player.Cooker.PutDownDish(putDonwPoint.transform, foodData))
		{
			DoEatingFood();
		}
	}

	private void DoEatingFood()
	{
		curCustomer.Order.CloseUI();

		WaitTime = Random.Range(30, 41);

		Coroutines coroutines = new Coroutines();
		StartCoroutine(coroutines.JustWaitRoutine(5, RateTaste));
	}

	public override void NextAction()
	{
		if (curCustomer.CurState == CustStateType.Eating) 
		{

		}
	}

	public override void ClearAction()
	{
	}

	public override void TakeActionAfterNoti()
	{

	}

	private void RateTaste()
	{
		//todo. ¸ÀÆò°¡ UI ¶ç¿ì±â

		Coroutines coroutines = new Coroutines();
		StartCoroutine(coroutines.JustWaitRoutine(10, Payment)); 
	}

	private void Payment()
	{
		//todo. ¸ÀÆò°¡ UI Áö¿ì±â

		GameManager.Data.Revenue += foodData.Price; //µ· ÁöºÒ

		curCustomer.Mover.OnExit?.Invoke();
	}
}
