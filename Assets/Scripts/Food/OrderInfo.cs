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
		CookResultType = CookedType.None;
	}

	public int OderID { get; set; }

	public FoodData FoodInfo { get; set; }

	/// <summary>
	/// 주문 접수 여부
	/// </summary>
	public bool IsOrder { get; set; }

	/// <summary>
	/// 조리 결과
	/// </summary>
	public CookedType CookResultType { get; set; }
}
