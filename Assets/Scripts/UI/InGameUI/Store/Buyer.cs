using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buyer : MonoBehaviour
{
	public void ClickedBuyBtn(IngrData item)
	{
		if (GameManager.Data.Revenue - item.Price >= 0)
		{
			GameManager.Data.Revenue -= item.Price;

			if (StorageManager.GetInstance().Ingredients.ContainsKey(item.Name))
				StorageManager.GetInstance().Ingredients[item.Name].Count++;
		}
		else
		{
			GuidMessageManager.GetInstance().ShowMessage("잔액이 부족하여 구매할 수 없습니다.");
		}
	}
}
