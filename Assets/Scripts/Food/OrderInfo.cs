using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderInfo
{
	public OrderInfo(FoodData foodInfo, bool isOrder = false)
	{
		FoodInfo = foodInfo;
		IsOrder = isOrder;
	}

	public int OderID { get; set; }

	public FoodData FoodInfo { get; set; }

	public bool IsOrder { get; set; }
}
