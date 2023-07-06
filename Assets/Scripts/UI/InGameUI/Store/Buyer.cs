using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Buyer<T> : MonoBehaviour where T : SaleItemData
{
	public UnityAction<T> OnBuy;

	public void ClickedBuyBtn(T item) 
	{
		if (GameManager.Data.Revenue - item.Price >= 0)
		{
			GameManager.Data.Revenue -= item.Price;

			OnBuy?.Invoke(item);
			//if (StorageManager.GetInstance().Ingredients.ContainsKey(item.Name))
			//	StorageManager.GetInstance().Ingredients[item.Name].Count++;
		}
		else
		{
			GuidMessageManager.GetInstance().ShowMessage("잔액이 부족하여 구매할 수 없습니다.");
		}
	}
}
