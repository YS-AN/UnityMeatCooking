using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HearthOven : MonoBehaviour, IMoveable, ICancelableOrder
{
	private const string UI_PATH = "UI/CookList";

	private CookListUI cookListUI = null;

	[SerializeField]
	private Transform stopPosition;
	public Vector3 StopPoint { get; set; }

	[SerializeField]
	private Transform[] cookPositions;
	private Stack<Transform> CookPoint;

	public UnityAction<FoodData> OnCooking;

	private CinemachineVirtualCamera cam2; //todo. 플레이어 카메라 무빙이 결정될 때 까지만 임시로...

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

		cam2 = GameObject.Find("CM vcam3").GetComponent<CinemachineVirtualCamera>();
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

	public void DoCooking(FoodData foodData)
	{
		if (CookPoint.Count > 0)
		{
			CloseUI();

			Transform cookPnt = CookPoint.Pop();

			Cook cookObj = GameManager.Resource.Load<Cook>(foodData.CookingObjectPath);
			var newCook = Instantiate(cookObj, cookPnt.position, cookPnt.rotation);
			newCook.transform.SetParent(FoodManager.GetInstance().transform, true);
			newCook.Cooker?.OnCooking();
		}
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