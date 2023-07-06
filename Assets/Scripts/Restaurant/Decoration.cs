using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Decoration : SceneUI
{
	private CinemachineVirtualCamera MainCam;

	public UnityAction OnStartDecoration;
	public UnityAction OnEndDecoration;

	public Transform Customers;

	protected override void Awake()
	{
		base.Awake();

		buttons["BtnClose"].onClick.AddListener(() => { EndOfDecoration(); });

		OnStartDecoration += DecorateRestaurant;

		transform.gameObject.SetActive(false);
		GameManager.Data.IsPlaceable = false;
	}

	public void DecorateRestaurant()
    {
		MainCam = CinemachineCore.Instance.GetActiveBrain(0).ActiveVirtualCamera as CinemachineVirtualCamera;

		if (MainCam != null)
			MainCam.Priority = 10;

		transform.gameObject.SetActive(true);
		Customers.GetComponent<CustomerSpawner>().IsOpenStore = false;

		//todo 모든 customer 삭제
		var customers = Customers.GetComponentsInChildren<Customer>();
		foreach(var cust in customers)
		{
			if(cust.CurState == CustStateType.Enter)
			{
				cust.Mover.OnRemove?.Invoke();
			}
			else
			{
				cust.Mover.OnExit?.Invoke();
			}
		}

		GameManager.Data.IsPlaceable = true;
	}

    private void EndOfDecoration()
    {
		GameManager.Data.IsPlaceable = false;
		transform.gameObject.SetActive(false);

		MainCam.Priority = 30;
		
		OnEndDecoration?.Invoke();

		Customers.GetComponent<CustomerSpawner>().IsOpenStore = true;
	}
}
