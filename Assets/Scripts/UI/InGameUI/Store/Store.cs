using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

		uiController = new StoreController(new StoreModel(SaleItemPrefab, SaleContent));
	}

	private void OnEnable()
	{
		uiController = new StoreController(new StoreModel(SaleItemPrefab, SaleContent));
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


public class StoreController : MonoBehaviour
{
	private StoreModel model;

	public StoreController(StoreModel model)
	{
		this.model = model;
	}

	public void SetSalesContent()
	{
		model.SalesItems = GameManager.Data.IsPlaceable ? GetSaleItemsFromFurniture() : GetSaleItemsFromIngredient();

		model.Items = model.SaleContent.GetComponentsInChildren<SaleItem>().ToList();

		if (model.Items.Count < model.SalesItems.Count)
			InitSaleContent(model.SalesItems.Count - model.Items.Count);

		else if (model.Items.Count > model.SalesItems.Count)
		{
			int count = model.Items.Count - model.SalesItems.Count;
			int max = model.Items.Count - 1;

			for (int i=0; i<count; i++)
				Destroy(model.Items[max - i].gameObject);

			model.Items = model.Items.Take(model.SalesItems.Count).ToList();
		}

		int index = 0;
		
		foreach (var item in model.Items)
		{
			item.SaleData = model.SalesItems[index++];
		}
		PlayerManager.GetInstance().Player.Camera.IsRotation = false;
	}

	public void ClearSalesContent()
	{
		PlayerManager.GetInstance().Player.Camera.IsRotation = true;
	}

	private void InitSaleContent(int createdCnt)
	{
		for (int i = 0; i < createdCnt; i++)
			CreateItem();

		model.Items = model.SaleContent.GetComponentsInChildren<SaleItem>().ToList();
	}

	private void CreateItem()
	{
		var newItem = Instantiate(model.SaleItemPrefab, Vector3.zero, Quaternion.identity);
		newItem.transform.SetParent(model.SaleContent);
	}

	private List<SaleItemData> GetSaleItemsFromIngredient()
	{
		List<SaleItemData> items = new List<SaleItemData>();

		foreach (var item in StorageManager.GetInstance().Ingredients)
		{
			SaleItemData data = new SaleItemData();

			data.ItemId = (int)item.Key;
			data.ItemIcon = item.Value.Data.Icon;
			data.ItemPrice = item.Value.Data.Price;

			items.Add(data);
		}

		return items;
	}

	private List<SaleItemData> GetSaleItemsFromFurniture()
	{
		List<SaleItemData> items = new List<SaleItemData>();

		foreach (var item in FurnitureManager.GetInstance().Furnitures)
		{
			SaleItemData data = new SaleItemData();

			data.ItemId = (int)item.Key;
			data.ItemIcon = item.Value.Icon;
			data.ItemPrice = item.Value.Price;

			items.Add(data);
		}
		return items;
	}
}

public class StoreModel
{
	public StoreModel(SaleItem item, Transform saleContent)
	{
		SaleContent = saleContent;
		SaleItemPrefab = item;
		Items = new List<SaleItem>();
	}

	public Transform SaleContent { get; set; }

	public SaleItem SaleItemPrefab;

	public List<SaleItem> Items { get; set; }

	public List<SaleItemData> SalesItems { get; set; }

	
}
