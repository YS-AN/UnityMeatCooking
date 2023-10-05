using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FurnStoreController : StoreController
{
	public FurnStoreController(StoreModel model) : base(model) { }

	protected override List<SaleItemData> GetSaleItemDatas()
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

	public override void SetSalesContent()
	{
		base.SetSalesContent();

		int index = 0;
		foreach (var item in model.Items)
		{
			item.SaleData = model.SalesItems[index++];
			item.BtnBuy.transform.GetComponent<Buyer>().OnBuy += BuyItem;
		}
	}


	/// <summary>
	/// 아이템 구매 시 동작
	/// </summary>
	/// <param name="item"></param>
	private void BuyItem(SaleItemData item)
	{
		if (item.ItemId == 0)
			FurnitureManager.GetInstance().Spawner.OnCreateFurniture?.Invoke(item);
		else
			FurnitureManager.GetInstance().Spawner.OnCreateFurniture?.Invoke(item);
	}

	public override void ClearSalesContent()
	{
		base.ClearSalesContent();

		foreach (var item in model.Items)
		{
			item.ClearButton();
			item.BtnBuy.transform.GetComponent<Buyer>().OnBuy -= BuyItem;
		}
	}

}
