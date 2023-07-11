using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaleItem : MonoBehaviour
{
	[SerializeField]
	private Image imgItem;

	[SerializeField]
	private TextMeshProUGUI txtPrice;

	[SerializeField]
	private Button btnBuy;
	public Button BtnBuy { get { return btnBuy; } }

	private SaleItemData _salesData;
	public SaleItemData SaleData { get { return _salesData; } set { _salesData = value; InitItemInfo(value); }  }

	private void InitItemInfo(SaleItemData saleData)
	{
		imgItem.sprite = saleData.ItemIcon;
		txtPrice.text = saleData.ItemPrice.ToString();

		btnBuy.onClick.AddListener(() => { btnBuy.transform.GetComponent<Buyer>().ClickedBuyBtn(saleData); });
	}
}
