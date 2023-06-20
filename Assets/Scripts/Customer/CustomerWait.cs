using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CustomerWait : MonoBehaviour
{
	public Customer curCustomer;

	public int WaitTime;

	public UnityAction<Customer> onWait;

	private void Awake()
	{
		onWait += Wait;
	}

	private void Wait(Customer cust)
	{
		curCustomer = cust;

		WaitBar waitBar = GameManager.UI.ShowInGameUI<WaitBar>("UI/WaitBar");
		waitBar.SetTarget(transform);
		waitBar.customer = curCustomer;

		waitBar.StartSlider(WaitTime);
	}
}
