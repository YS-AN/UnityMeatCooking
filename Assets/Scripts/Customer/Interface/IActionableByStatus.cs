using System;
using UnityEngine;

public interface IActionableByStatus
{
	public Vector3 StopPoint { get; set; }

	public void NextAction();

	public void StateAction(Customer cust, CustStateType type);
}