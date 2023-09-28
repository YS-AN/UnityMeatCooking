using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class HearthOven : MonoBehaviour
{
	private const string UI_PATH = "UI/CookList";

	private CookListUI cookListUI = null;

	[SerializeField]
	private Transform CookObject;

	[SerializeField]
	private Transform[] cookPositions;
	private Stack<Transform> CookPoint;

	public UnityAction<OrderInfo> OnCooking;

	private void Awake()
	{
		OnCooking += DoCooking;

		CookPoint = new Stack<Transform>();
		foreach (var point in cookPositions)
		{
			CookPoint.Push(point);
		}

		//FoodManager.GetInstance().AddObserver(this);
	}

	public void OpenCookListUI()
	{
		cookListUI = GameManager.UI.ShowInGameUI<CookListUI>(UI_PATH);
		cookListUI.SetTarget(transform);
		cookListUI.SetCurrentOven(this);
	}

	public void CloseCookListUI()
	{
		if (cookListUI != null)
		{
			GameManager.UI.CloseInGameUI(cookListUI);
			cookListUI = null;
		}
	}

	private void DoCooking(OrderInfo orderData)
	{
		if (CookPoint.Count > 0)
		{
			CloseCookListUI();

			orderData.CookingPoint = CookPoint.Pop();

			var newCook = GameManager.Resource.Instantiate<Cook>(orderData.FoodInfo.CookingObjectPath, orderData.CookingPoint.position, orderData.CookingPoint.rotation);
			newCook.transform.SetParent(CookObject, true);
			newCook.Cooker.OnFinishedCook += FinishedCook;
			newCook.Cooker?.OnCooking(orderData);
		}
	}

	private void FinishedCook(OrderInfo orderData)
	{
		CookPoint.Push(orderData.CookingPoint);
		PlayerManager.GetInstance().Player.Cooker.HoldDish(orderData);
	}
}