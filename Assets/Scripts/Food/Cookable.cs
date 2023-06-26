using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cookable : MonoBehaviour
{
	public FoodData FoodData;
	public HearthOven Oven;

	private Button btnCook;

	private bool isCooking;

	public void Awake()
	{
		btnCook = transform.GetComponent<Button>();
		btnCook.onClick.AddListener(() => DoCooking());

		isCooking = false;
	}

	public void DoCooking()
	{
		if (FoodData != null)
		{
			Oven.OnCooking(FoodData);
		}
	}
}
