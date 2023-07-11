using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngrStoreController : StoreController
{
	public IngrStoreController(StoreModel model) : base(model) { }

	protected override List<SaleItemData> GetSaleItemDatas()
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
		if (StorageManager.GetInstance().Ingredients.ContainsKey((IngredientName)item.ItemId))
			StorageManager.GetInstance().Ingredients[(IngredientName)item.ItemId].Count++;
	}

	public override void ClearSalesContent()
	{
		base.ClearSalesContent();

		foreach (var item in model.Items)
		{
			item.BtnBuy.transform.GetComponent<Buyer>().OnBuy -= BuyItem;
		}
	}
}