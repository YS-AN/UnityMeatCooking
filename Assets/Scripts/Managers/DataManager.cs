using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DataManager : MonoBehaviour
{
	private int revenue;
	public int Revenue { get { return revenue; } set { revenue = value; OnChangeRevenue?.Invoke(); } }
	public string RevenueToStr { get { return revenue == 0 ? "0" : string.Format("{0:0,0}", revenue); } }
	public event UnityAction OnChangeRevenue;

	/// <summary>
	/// ���� �Ͻ����� �������� ����
	/// </summary>
	public bool IsGamePause { get; set; } = false;

	/// <summary>
	/// ���� � ������ Ȯ��
	/// </summary>
	public bool IsOpenrestaurant { get; set; } = false;
}
