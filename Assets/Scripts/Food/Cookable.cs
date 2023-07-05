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
			else
			{
				GuidMessageManager.GetInstance().ShowMessage($"{OrderData.FoodInfo.Name} 만들기에는 재료가 부족합니다. \n창고에서 재료를 가져온 뒤 다시 실행해주세요");
			}
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
