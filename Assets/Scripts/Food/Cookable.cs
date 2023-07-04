using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Cookable : MonoBehaviour
{
	public int FoodId;
	public OrderInfo OrderData;
	public HearthOven Oven;

	private Button btnCook;

	//private bool isCooking;

	public void Awake()
	{
		btnCook = transform.GetComponent<Button>();
		btnCook.onClick.AddListener(() => DoCooking());

		//isCooking = false;
	}

	public void DoCooking()
	{
		if (OrderData != null)
		{
			if (IsCookable(OrderData.FoodInfo.Recipe))
			{
				GetIngredients(OrderData.FoodInfo.Recipe);

				Oven.OnCooking?.Invoke(OrderData);
				ClearButton();
			}
		}
		else
		{
			//todo. 재료 부족 메시지 보내기 
		}
	}

	private bool IsCookable(List<Recipe> recipe)
	{
		foreach (Recipe item in recipe)
		{
			if (StorageManager.GetInstance().HavingList.ContainsKey(item.Name) == false)
				return false;

			if (StorageManager.GetInstance().HavingList[item.Name].Count - item.Count < 0)
				return false;
		}
		return true;
	}

	private void GetIngredients(List<Recipe> recipe)
	{
		foreach (Recipe item in recipe)
		{
			StorageManager.GetInstance().HavingList[item.Name].Count -= item.Count;

			if(StorageManager.GetInstance().HavingList[item.Name].Count <= 0)
				StorageManager.GetInstance().HavingList.Remove(item.Name);
		}
	}

	public void ClearButton()
	{
		btnCook.image.sprite = null;
		FoodManager.GetInstance().RemoveOrder(FoodId);
		OrderData = null;
	}
}
