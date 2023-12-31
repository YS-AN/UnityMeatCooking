using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;
using UnityEngine.UI;

public class OrderInfo
{
	public OrderInfo(FoodData foodInfo, Customer customer = null, bool isOrder = false)
	{
		OderID = Guid.NewGuid().ToString();
		FoodInfo = foodInfo;
		Orderer = customer;
		IsOrder = isOrder;
		CookResultType = CookedType.None;
	}

	public string OderID { get; private set; }

	public FoodData FoodInfo { get; set; }

	/// <summary>
	/// 주문 접수 여부
	/// </summary>
	public bool IsOrder { get; set; }

	/// <summary>
	/// 조리 결과
	/// </summary>
	public CookedType CookResultType { get; set; }

	/// <summary>
	/// 조리 위치
	/// </summary>
	public Transform CookingPoint { get; set; }

	/// <summary>
	/// 주문자
	/// </summary>
	public Customer Orderer { get; set; }
}
