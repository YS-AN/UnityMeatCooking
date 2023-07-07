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
			GuidMessageManager.GetInstance().ShowMessage("�ܾ��� �����Ͽ� ������ �� �����ϴ�.");
		}
	}
}
