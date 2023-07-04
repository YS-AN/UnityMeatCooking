using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class OpenStoreUI : SceneUI
{
	private string UI_PATH = "UI/StoreUI";
	private Store store;

	protected override void Awake()
	{
		base.Awake();

		buttons["BtnStore"].onClick.AddListener(() => { OpenStore(); });
	}

	private void OpenStore()
	{
		Store store = GameManager.UI.ShowInGameUI<Store>(UI_PATH);
	}
}

