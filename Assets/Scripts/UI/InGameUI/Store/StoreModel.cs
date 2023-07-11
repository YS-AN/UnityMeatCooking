using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreModel
{
	public StoreModel(SaleItem item, Transform saleContent)
	{
		SaleContent = saleContent;
		SaleItemPrefab = item;
		Items = new List<SaleItem>();
	}

	/// <summary>
	/// store ui list content object
	/// </summary>
	public Transform SaleContent { get; set; }

	/// <summary>
	/// list object 
	/// </summary>
	public SaleItem SaleItemPrefab;

	/// <summary>
	/// list data
	/// </summary>
	public List<SaleItem> Items { get; set; }

	/// <summary>
	/// setting data
	/// </summary>
	public List<SaleItemData> SalesItems { get; set; }
}