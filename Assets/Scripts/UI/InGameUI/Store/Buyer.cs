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
			GuidMessageManager.GetInstance().ShowMessage("�ܾ��� �����Ͽ� ������ �� �����ϴ�.");
		}
	}
}
