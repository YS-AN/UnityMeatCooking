using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
			Oven.OnCooking?.Invoke(OrderData);
			ClearButton();
		}
	}

	public void ClearButton()
	{
		btnCook.image.sprite = null;
		FoodManager.GetInstance().RemoveOrder(FoodId);
		OrderData = null;
	}
}
