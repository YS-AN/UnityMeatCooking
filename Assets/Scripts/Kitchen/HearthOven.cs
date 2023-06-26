using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HearthOven : MonoBehaviour, IMoveable
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

	private void Awake()
	{
		StopPoint = stopPosition.position;
		OnCooking += DoCooking;

		CookPoint = new Stack<Transform>();
		foreach (var point in cookPositions)
		{
			CookPoint.Push(point);
		}
	}

	public void NextAction()
	{
		cookListUI = GameManager.UI.ShowInGameUI<CookListUI>(UI_PATH);
		cookListUI.SetTarget(transform);
		cookListUI.SetCurrentOven(this);
	}

	public void ClearAction()
	{
		if(cookListUI != null)
		{
			GameManager.UI.CloseInGameUI(cookListUI);
			cookListUI = null;
		}
	}

	public void DoCooking(FoodData foodData)
	{
		if(CookPoint.Count > 0)
		{
			Transform cookPnt = CookPoint.Pop();

			Cook cookObj = GameManager.Resource.Load<Cook>(foodData.CookingObjectPath);
			var newCook = Instantiate(cookObj, cookPnt.position, cookPnt.rotation);
			newCook.transform.SetParent(FoodManager.GetInstance().transform, true);
			newCook.Cooker?.OnCooking(newCook);
		}
	}
}
