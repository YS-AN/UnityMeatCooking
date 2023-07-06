using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using static UnityEditor.Progress;

public class OpenStoreUI : SceneUI
{
	private OpenStoreController uiController;

	protected override void Awake()
	{
		base.Awake();

		uiController = new OpenStoreController(new OpenStoreModel());

		buttons["BtnStore"].onClick.AddListener(() => { uiController.OpenStore(); });
	}
}

public class OpenStoreController
{
	private OpenStoreModel model;

	public OpenStoreController(OpenStoreModel model)
	{
		this.model = model;
	}

	public void OpenStore()
	{
		Store store = GameManager.UI.ShowInGameUI<Store>(model.UI_PATH);

	}
}

public class OpenStoreModel
{
	public string UI_PATH = "UI/StoreUI";

	public List<SaleItemData> Items;
}

