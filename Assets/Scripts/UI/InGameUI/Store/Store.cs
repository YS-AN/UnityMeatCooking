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

	StoreController uiController;

	protected override void Awake()
	{
		base.Awake();

		buttons["BtnClose"].onClick.AddListener(() => { CloseStore(); });

		uiController = new StoreController(new StoreModel());
		uiController.SetSaleItems(SaleContent.GetComponentsInChildren<SaleItem>());
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
		int max = StorageManager.GetInstance().Ingredients.Count;
		foreach (var item in model.Items)
		{
			item.Ingredient = StorageManager.GetInstance().Ingredients[(IngredientName)index++].Data;

			if (index == max)
				break;
		}
	}

	public void ClearSalesContent()
	{
	}
}

public class StoreModel
{
	public SaleItem[] Items { get; set; }
}
