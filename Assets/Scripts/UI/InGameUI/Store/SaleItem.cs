using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SaleItemData
{
	public int ItemId; 

	public Sprite ItemIcon;

	public int ItemPrice;
}

public class SaleItem : MonoBehaviour
{
	[SerializeField]
	private Image ImgItem;

	[SerializeField]
	private TextMeshProUGUI TxtPrice;

	[SerializeField]
	private Button BtnBuy;

	private SaleItemData _salesData;
	public SaleItemData SaleData { get { return _salesData; } set { _salesData = value; InitItemInfo(value); }  }

	private void InitItemInfo(SaleItemData saleData)
	{
		ImgItem.sprite = saleData.ItemIcon;
		TxtPrice.text = saleData.ItemPrice.ToString();

		BtnBuy.onClick.AddListener(() => { BtnBuy.transform.GetComponent<Buyer>().ClickedBuyBtn(saleData); });
	}
}
