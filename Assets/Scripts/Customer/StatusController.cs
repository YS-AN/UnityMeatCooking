using UnityEngine;
using UnityEngine.Events;

public abstract class StatusController : ExitController, IActionableByStatus
{
	public UnityAction<Customer, CustStateType> OnStateAction;

	public Customer curCustomer;

	public int WaitTime;

	private Vector3 _stopPoint;
	public Vector3 StopPoint
	{
		get { return _stopPoint; }
		set 
		{ 
			_stopPoint = value; 
			curCustomer.Status.OnChangeStopPoint?.Invoke(_stopPoint); 
		}
	}

	protected virtual void Awake()
	{
		OnStateAction += StateAction;
	}

	public virtual void StateAction(Customer cust, CustStateType type)
	{
		curCustomer = cust;
		curCustomer.CurState = type;
	}

	public abstract void NextAction();

	public abstract void ClearAction();
}