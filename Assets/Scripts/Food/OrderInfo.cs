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
	/// �ֹ� ���� ����
	/// </summary>
	public bool IsOrder { get; set; }

	/// <summary>
	/// ���� ���
	/// </summary>
	public CookedType CookResultType { get; set; }
}
