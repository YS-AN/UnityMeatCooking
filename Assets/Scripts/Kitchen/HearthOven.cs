using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class HearthOven : MonoBehaviour, IMoveable, ICancelableOrder
{
	private const string UI_PATH = "UI/CookList";

	private CookListUI cookListUI = null;

	[SerializeField]
	public Transform stopPosition;
	public Vector3 StopPoint { get; set; }

	[SerializeField]
	private Transform[] cookPositions;
	private Stack<Transform> CookPoint;

	public UnityAction<OrderInfo> OnCooking;

	private CinemachineVirtualCamera cam2; //todo. 플레이어 카메라 무빙이 결정될 때 까지만 임시로...

	private HearthOvenMover mover = null;

	private void Awake()
	{
		StopPoint = stopPosition.position;
		OnCooking += DoCooking;

		CookPoint = new Stack<Transform>();
		foreach (var point in cookPositions)
		{
			CookPoint.Push(point);
		}

		FoodManager.GetInstance().AddObserver(this);

		cam2 = GameObject.Find("Cam_Oven").GetComponent<CinemachineVirtualCamera>();

		this.AddComponent<HearthOvenMover>();
		mover = GetComponent<HearthOvenMover>();
	}

	public void NextAction()
	{
		cam2.Priority = 30;

		cookListUI = GameManager.UI.ShowInGameUI<CookListUI>(UI_PATH);
		cookListUI.SetTarget(transform);
		cookListUI.SetCurrentOven(this);
	}

	public void ClearAction()
	{
		cam2.Priority = 10;

		CloseUI();
	}

	private void DoCooking(OrderInfo orderData)
	{
		if (CookPoint.Count > 0)
		{
			CloseUI();

			orderData.CookingPoint = CookPoint.Pop();

			var newCook = GameManager.Resource.Instantiate<Cook>(orderData.FoodInfo.CookingObjectPath, orderData.CookingPoint.position, orderData.CookingPoint.rotation);
			newCook.transform.SetParent(FoodManager.GetInstance().transform, true);
			newCook.Cooker.OnFinishedCook += FinishedCook;
			newCook.Cooker?.OnCooking(orderData);
		}
	}

	private void FinishedCook(OrderInfo orderData)
	{
		CookPoint.Push(orderData.CookingPoint);
		PlayerManager.GetInstance().Player.Cooker.HoldDish(orderData);
	}

	private void CloseUI()
	{
		if (cookListUI != null)
		{
			GameManager.UI.CloseInGameUI(cookListUI);
			cookListUI = null;
		}
	}

	public void TakeActionAfterNoti()
	{
		Debug.Log("주문한 메뉴가 삭제 됨!");
		//주문내역에 변동사항이 생김
	}
}