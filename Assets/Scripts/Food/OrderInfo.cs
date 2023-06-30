using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;
using UnityEngine.UI;

public class OrderInfo
{
	public OrderInfo(FoodData foodInfo, bool isOrder = false)
	{
		OderID = Guid.NewGuid().ToString();
		FoodInfo = foodInfo;
		IsOrder = isOrder;
		CookResultType = CookedType.None;
	}

	public string OderID { get; private set; }

	public FoodData FoodInfo { get; set; }

	/// <summary>
	/// �ֹ� ���� ����
	/// </summary>
	public bool IsOrder { get; set; }

	/// <summary>
	/// ���� ���
	/// </summary>
	public CookedType CookResultType { get; set; }

	/// <summary>
	/// ���� ��ġ
	/// </summary>
	public Transform CookingPoint { get; set; }
}
