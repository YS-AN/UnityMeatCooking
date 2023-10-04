using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Store : InGameUI
{
	const string UI_PATH = "UI/SaleItem";

	[SerializeField]
	private Transform SaleContent;

	[SerializeField]
	private SaleItem SaleItemPrefab;

	private StoreController uiController;

	protected override void Awake()
	{
		base.Awake();
		buttons["BtnClose"].onClick.AddListener(() => { CloseStore(); });
	}

	private void OnEnable()
	{
		StoreModel model = new StoreModel(SaleItemPrefab, SaleContent);
		uiController = GameManager.Data.IsOpenRestaurant ? new IngrStoreController(model) : new FurnStoreController(model);
		uiController.SetSalesContent();
	}

	private void OnDisable()
	{
		uiController.ClearSalesContent();
	}

	private void CloseStore()
	{
		GameManager.UI.CloseInGameUI(this);
	}
}


