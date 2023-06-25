using UnityEngine;
using UnityEngine.Events;

public abstract class CustomerState : MonoBehaviour, IMoveable
{
	public UnityAction<Customer, CustStateType> OnStateAction;

	public Customer curCustomer;

	public int WaitTime;

	public Vector3 StopPoint { get; set; }

	protected virtual void Awake()
	{
		OnStateAction += StateAction;
	}

	protected virtual void StateAction(Customer cust, CustStateType type)
	{
		curCustomer = cust;
		curCustomer.CurState = type;
	}

	public abstract void NextAction();

	public abstract void ClearAction();
}