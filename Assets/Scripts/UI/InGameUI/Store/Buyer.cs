using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buyer : MonoBehaviour
{
	public void ClickedBuyBtn(IngrData item)
	{
		Debug.Log("ClickedBuyBtn" + item.Name);

		if(GameManager.Data.Revenue - item.Price >= 0)
		{
			GameManager.Data.Revenue -= item.Price;

			if(StorageManager.GetInstance().Ingredients.ContainsKey(item.Name))
				StorageManager.GetInstance().Ingredients[item.Name].Count++;
		}
	}
}
