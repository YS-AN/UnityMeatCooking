using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerInfo
{
	/// <summary>
	/// 대기 시간 : 주문까지 대기시간
	/// </summary>
	public int WaitTime_Seat;

	/// <summary>
	/// 대기 시간 : 주문 후 대기시간
	/// </summary>
	public int WaitTime_Order { get { return WaitTime_Seat * 2; } }

	/// <summary>
	/// 식사시간
	/// </summary>
	public int EatingTime { get { return 30; } }

	/// <summary>
	/// 주문 할 음식
	/// </summary>
	public FoodData FoodToOrder;
}
