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

			if(GameManager.Data.IsPlaceable)
			{
				//todo. �ϴÿ��� ������ ���ϰ� ����������
			}
			else
			{
				if (StorageManager.GetInstance().Ingredients.ContainsKey((IngredientName)item.ItemId))
					StorageManager.GetInstance().Ingredients[(IngredientName)item.ItemId].Count++;
			}

		}
		else
		{
			GuidMessageManager.GetInstance().ShowMessage("�ܾ��� �����Ͽ� ������ �� �����ϴ�.");
		}
	}
}
