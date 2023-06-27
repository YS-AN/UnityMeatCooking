using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class CookRange : MonoBehaviour
{
	private const string UI_PATH = "UI/CookList";

	[SerializeField]
	private LayerMask PlayerMask;

	/*
	CookListUI cookListPopUp = null;

	
	private void OnTriggerEnter(Collider other)
	{
		

		if (PlayerMask.IsContain(other.gameObject.layer))
		{
			cookListPopUp = GameManager.UI.ShowInGameUI<CookListUI>(UI_PATH);
			cookListPopUp.SetTarget(transform);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		GameManager.UI.CloseInGameUI(cookListPopUp);
	}
	//*/
}
