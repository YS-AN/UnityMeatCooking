using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Buyer : MonoBehaviour 
{
	public UnityAction<SaleItemData> OnBuy;

	public void ClickedBuyBtn(SaleItemData item) 
	{
		if (GameManager.Data.Revenue - item.ItemPrice >= 0)
		{
			GameManager.Data.Revenue -= item.ItemPrice;

			OnBuy?.Invoke(item);
		}
		else
		{
			GuidMessageManager.GetInstance().ShowMessage("잔액이 부족하여 구매할 수 없습니다.");
		}
	}
}
