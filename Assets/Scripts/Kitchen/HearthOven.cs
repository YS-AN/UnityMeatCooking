using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearthOven : MonoBehaviour, IMoveable
{
	private const string COOK_UI_PATH = "UI/CookList";

	private CookListUI cookListPopUp = null;

	[SerializeField]
	private Transform StopPosition;
	public Vector3 StopPoint { get; set; }

	private void Awake()
	{
		StopPoint = StopPosition.position;
	}

	public void NextAction()
	{
		cookListPopUp = GameManager.UI.ShowInGameUI<CookListUI>(COOK_UI_PATH);
		cookListPopUp.SetTarget(transform);
	}

	public void ClearAction()
	{
		if(cookListPopUp != null)
		{
			GameManager.UI.CloseInGameUI(cookListPopUp);
			cookListPopUp = null;
		}
			
	}

}
