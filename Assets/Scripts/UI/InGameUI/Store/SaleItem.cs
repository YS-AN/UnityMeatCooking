using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaleItem : MonoBehaviour
{
	[SerializeField]
	private Image ImgItem;

	[SerializeField]
	private TextMeshProUGUI TxtPrice;

	[SerializeField]
	private Button BtnBuy;

	private IngrData _ingredient;
	public IngrData Ingredient { get { return _ingredient; } set { _ingredient = value; InitItemInfo(value); }  }

	private void InitItemInfo(IngrData ingrData)
	{
		ImgItem.sprite = ingrData.Icon;
		TxtPrice.text = ingrData.Price.ToString();

		BtnBuy.onClick.AddListener(() => { BtnBuy.transform.GetComponent<Buyer>().ClickedBuyBtn(ingrData); });
	}
}
