using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ExitController : MonoBehaviour, IExitable
{
	protected CustomerExit customerExit;

	protected virtual void Start()
	{
		customerExit = GetComponent<CustomerExit>();
		customerExit.AddObserver(this);
	}

	/// <summary>
	/// �մ� ���� �� �ൿ
	/// </summary>
	public abstract void TakeActionAfterNoti();
}
