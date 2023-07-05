using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomerStatus : MonoBehaviour, IMoveable
{
	//todo. ClearAction 동작보다 customer의 destory가 더 빨라서 오류나 나면 어떡하지?? 이거 어떻게 해결함?? ㅠㅛㅠ

	public Vector3 StopPoint { get; set; }

	public UnityAction<Vector3> OnChangeStopPoint;

	private void Awake()
	{
		OnChangeStopPoint += ChangeStopPoint;
	}

	private void ChangeStopPoint(Vector3 point)
	{
		StopPoint = point;
	}

	public void ClearAction()
	{
		GetStatusComponent().ClearAction();
	}

	public void NextAction()
	{
		GetStatusComponent().NextAction();
	}

	private StatusController GetStatusComponent()
	{
		Customer customer = transform.GetComponent<Customer>();

		switch (customer.CurState)
		{
			case CustStateType.Wait:
				return customer.Wait;

			case CustStateType.Order:
				return customer.Order;

			case CustStateType.Eating:
				return customer.Eater;

			default:
				return default;
		}
	}
}