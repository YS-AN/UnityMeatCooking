using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CustomerWait : StatusController
{
	private const string UI_PATH = "UI/Waiting";

	private WaitBar waitBar;

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

		StopPoint = cust.Mover.info.Chair.StopPoint.position;

		waitBar = GameManager.UI.ShowInGameUI<WaitBar>(UI_PATH);
		waitBar.SetTarget(transform);
		waitBar.customer = curCustomer;

		WaitTime = Random.Range(30, 41); //대기 시간은 30~40초 사이
		waitBar.StartSlider(WaitTime);
	}

	public override void NextAction()
	{
		if(waitBar != null)
		{
			waitBar.StopSlider();
			waitBar = null;
		}
		curCustomer.Order.OnStateAction?.Invoke(curCustomer, CustStateType.Order);
	}

	public override void ClearAction()
	{
	}

	public override void TakeActionAfterNoti()
	{
		if (waitBar != null)
		{
			GameManager.UI.CloseInGameUI(waitBar);
			waitBar = null;
		}
	}
}
