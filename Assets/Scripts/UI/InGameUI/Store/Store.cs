using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Store : InGameUI
{
	[SerializeField]
	private Transform SaleContent;

	[SerializeField]
	private SaleItem SaleItemPrefab;

	StoreController uiController;

	protected override void Awake()
	{
		base.Awake();
		buttons["BtnClose"].onClick.AddListener(() => { CloseStore(); });

		uiController = new StoreController(new StoreModel());

		InitSaleContent();
	}

	private void OnEnable()
	{
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

	private void InitSaleContent()
	{
		int ingrCnt = StorageManager.GetInstance().Ingredients.Count;
		for (int i = 0; i < ingrCnt; i++)
			CreateItem();

		uiController.SetSaleItems(SaleContent.GetComponentsInChildren<SaleItem>());
	}

	private void CreateItem()
	{
		var newItem = Instantiate(SaleItemPrefab, Vector3.zero, Quaternion.identity);
		newItem.transform.SetParent(SaleContent);
	}
}


public class StoreController
{
	private StoreModel model;

	public StoreController(StoreModel model)
	{
		this.model = model;
	}

	public void SetSaleItems(SaleItem[] saleItems)
	{
		model.Items = saleItems; 
	}

	public void SetSalesContent()
	{
		int index = 0;
		
		foreach (var item in model.Items)
		{
			item.Ingredient = StorageManager.GetInstance().Ingredients[(IngredientName)index++].Data;
		}
		PlayerManager.GetInstance().Player.Camera.IsRotation = false;
	}

	public void ClearSalesContent()
	{
		PlayerManager.GetInstance().Player.Camera.IsRotation = true;
	}
}

public class StoreModel
{
	public SaleItem[] Items { get; set; }
}
