using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerInfo
{
	/// <summary>
	/// ��� �ð� : �ֹ����� ���ð�
	/// </summary>
	public int WaitTime_Seat;

	/// <summary>
	/// ��� �ð� : �ֹ� �� ���ð�
	/// </summary>
	public int WaitTime_Order { get { return WaitTime_Seat * 2; } }

	/// <summary>
	/// �Ļ�ð�
	/// </summary>
	public int EatingTime { get { return 30; } }

	/// <summary>
	/// �ֹ� �� ����
	/// </summary>
	public FoodData FoodToOrder;
}
