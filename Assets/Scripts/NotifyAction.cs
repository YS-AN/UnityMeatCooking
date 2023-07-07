using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IObservable
{
	public void TakeActionAfterNoti();
}

public class NotifyContorller<T> : MonoBehaviour where T : IObservable
{
	private List<T> observers;

	public UnityAction OnNotifyAction;

	protected virtual void Awake()
	{
		observers = new List<T>();
		OnNotifyAction += DoNotifiedAction;
	}

	public void AddObserver(T observer)
	{
		if (observers == null)
			observers = new List<T>();

		observers.Add(observer);
	}

	public void RemoveObserver(T observer)
	{
		if (observers != null && observers.Count > 0)
			observers.Remove(observer);
	}

	private void NotifySubscribers()
	{
		foreach (var observer in observers)
		{
			if (observer != null && ReferenceEquals(observer, null))
				return;

			observer.TakeActionAfterNoti();
		}
	}

	private void DoNotifiedAction()
	{
		NotifySubscribers();
	}


}
