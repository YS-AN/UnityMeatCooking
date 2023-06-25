using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CustomerWait : CustomerState
{
	private const string UI_PATH = "UI/Waiting";

	private WaitBar waitBar;

	protected override void Awake()
	{ 
		base.Awake();
	}

	protected override void StateAction(Customer cust, CustStateType type)
	{
		base.StateAction(cust, type);

		Debug.Log(curCustomer.CurState);

		StopPoint = cust.Mover.info.Chair.StopPoint.position;

		waitBar = GameManager.UI.ShowInGameUI<WaitBar>(UI_PATH);
		waitBar.SetTarget(transform);
		waitBar.customer = curCustomer;

		WaitTime = Random.Range(30, 41); //대기 시간은 30~40초 사이
		waitBar.StartSlider(WaitTime);
	}

	//public void OnPointerClick(PointerEventData eventData)
	//{
	//
	//}

	public override void NextAction()
	{ 
		if(curCustomer.CurState == CustStateType.Wait)
		{
			waitBar.StopSlider();
			curCustomer.Order.OnStateAction?.Invoke(curCustomer, CustStateType.Order);
		}
	}

	public override void ClearAction()
	{
	}

	public void Set(CustStateType type)
	{
		curCustomer.CurState = type;
	}
}
