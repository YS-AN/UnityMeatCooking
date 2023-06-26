using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class OrderingUI : InGameUI
{
	private const string NM_FOOD_BTN = "BtnFood";
	private const string NM_WAITBAR = "WaitBar";
	private const string NM_WAIT_LAYER = "Waiting";

	private FoodData foodData;

	private CookListUI cookListUI;

	protected override void Awake()
	{
		base.Awake();

		buttons[NM_FOOD_BTN].onClick.AddListener(() => { CheckOrder(); });
	}

	public void SetFood(FoodData foodData)
	{ 
		this.foodData = foodData;
		buttons[NM_FOOD_BTN].image.sprite = foodData.Icon;
	}

	public void StartWait(Customer curCust, int waitMax, int waitTime = 1)
	{
		WaitBar waitBar = images[NM_WAIT_LAYER].GetComponent<WaitBar>();
		//waitBar.SetTarget(transform);
		waitBar.customer = curCust;

		waitBar.StartSlider(waitMax, waitTime);
	}

	public void StopWait()
	{
		//waitBar.StopSlider();
	}

	public void CheckOrder()
	{
		if(foodData.IsOrder == false)
			FoodManager.GetInstance().AddOrder(foodData);
	}

}
