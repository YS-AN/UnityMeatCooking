using UnityEngine;
using UnityEngine.Events;

public abstract class MoveController : MonoBehaviour, IMoveable, IExitable
{
	protected CustomerExit customerExit;

	protected virtual void Start()
	{
		customerExit = GetComponent<CustomerExit>();
		customerExit.AddObserver(this);
	}

	public Vector3 StopPoint { get; set; }

	public abstract void NextAction();

	public abstract void ClearAction();

	public abstract void TakeActionAfterNoti();
}

public abstract class CustomerState : MoveController
{
	public UnityAction<Customer, CustStateType> OnStateAction;

	public Customer curCustomer;
	

	public int WaitTime;
	
	protected virtual void Awake()
	{
		OnStateAction += StateAction;
	}

	protected virtual void StateAction(Customer cust, CustStateType type)
	{
		curCustomer = cust;
		curCustomer.CurState = type;
	}
}