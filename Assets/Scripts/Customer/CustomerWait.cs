using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CustomerWait : MonoBehaviour, IPointerClickHandler, IMoveable
{
	private const string UI_PATH = "UI/Waiting";

	public Customer curCustomer;

	public int WaitTime;

	public UnityAction<Customer> onWait;

	public Vector3 StopPoint { get; set; }

	private void Awake()
	{
		onWait += Wait;
	}

	private void Wait(Customer cust)
	{
		curCustomer = cust;

		StopPoint = cust.Mover.info.Chair.StopPoint.position;

		WaitBar waitBar = GameManager.UI.ShowInGameUI<WaitBar>(UI_PATH);
		waitBar.SetTarget(transform);
		waitBar.customer = curCustomer;

		waitBar.StartSlider(WaitTime);
	}

	public void OnPointerClick(PointerEventData eventData)
	{

	}

	public void NextAction()
	{
		curCustomer.Order.OnOrder?.Invoke(curCustomer);
	}

	public void ClearAction()
	{
	}
}
