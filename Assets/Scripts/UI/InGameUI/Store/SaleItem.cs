using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SaleInfo<T> where T : SaleItemData
{
	private Image ImgItem;
	private TextMeshProUGUI TxtPrice;
	private Button BtnBuy;

	private T _salesData;
	public T SaleData { get { return _salesData; } set { _salesData = value; InitItemInfo(value); } }

	public SaleInfo(Image item, TextMeshProUGUI price, Button buy)
	{
		ImgItem = item;
		TxtPrice = price;
		BtnBuy = buy;
	}

	private void InitItemInfo(T saleData)
	{
		ImgItem.sprite = saleData.Icon;
		TxtPrice.text = saleData.Price.ToString();

		BtnBuy.onClick.AddListener(() => { BtnBuy.transform.GetComponent<Buyer<T>>().ClickedBuyBtn(saleData); });
	}
}


public class SaleItem<T> : MonoBehaviour where T : SaleItemData
{
	[SerializeField]
	private Image ImgItem;

	[SerializeField]
	private TextMeshProUGUI TxtPrice;

	[SerializeField]
	private Button BtnBuy;

	private T _salesData;
	public T SaleData { get { return _salesData; } set { _salesData = value; InitItemInfo(value); }  }

	private void InitItemInfo(T saleData)
	{
		ImgItem.sprite = saleData.Icon;
		TxtPrice.text = saleData.Price.ToString();

		BtnBuy.onClick.AddListener(() => { BtnBuy.transform.GetComponent<Buyer<T>>().ClickedBuyBtn(saleData); });
	}
}
