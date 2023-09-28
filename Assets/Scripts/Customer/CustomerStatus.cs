using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomerStatus : MonoBehaviour, IActionable
{
	public Transform StopPoint { get; set; }

	public UnityAction<Transform> OnChangeStopPoint;

	private void Awake()
	{
		OnChangeStopPoint += ChangeStopPoint;
	}

	private void ChangeStopPoint(Transform point)
	{
		StopPoint = point;
	}

	public void ClearAction()
	{
		GetStatusComponent()?.ClearAction();
	}

	public void NextAction()
	{
		GetStatusComponent()?.NextAction();
	}

	private StatusController GetStatusComponent()
	{
		Customer customer = transform.GetComponent<Customer>();

		if(customer != null)
		{
			switch (customer.CurState)
			{
				case CustStateType.Wait:
					return customer.Wait;

				case CustStateType.Order:
					return customer.Order;

				case CustStateType.Eating:
					return customer.Eater;
					
			}
		}
		return default;
	}
}