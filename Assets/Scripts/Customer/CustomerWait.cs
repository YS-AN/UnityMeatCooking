using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CustomerWait : MonoBehaviour, IPointerClickHandler, IMoveable
{
	private const string UI_PATH = "UI/Waiting";

	public Customer curCustomer;

	private int _waitTime;
	public int WaitTime { get { return _waitTime; } }

	public UnityAction<Customer> onWait;

	public Vector3 StopPoint { get; set; }

	private WaitBar waitBar;

	private void Awake()
	{
		onWait += Wait;
	}

	private void Wait(Customer cust)
	{
		curCustomer = cust;
		curCustomer.CurState = CustStateType.Wait;

		StopPoint = cust.Mover.info.Chair.StopPoint.position;

		waitBar = GameManager.UI.ShowInGameUI<WaitBar>(UI_PATH);
		waitBar.SetTarget(transform);
		waitBar.customer = curCustomer;

		_waitTime = Random.Range(30, 41); //대기 시간은 30~40초 사이
		waitBar.StartSlider(WaitTime);
	}

	public void OnPointerClick(PointerEventData eventData)
	{

	}

	public void NextAction()
	{
		waitBar.StopSlider();
		curCustomer.Order.OnOrder?.Invoke(curCustomer);
	}

	public void ClearAction()
	{
	}
}
