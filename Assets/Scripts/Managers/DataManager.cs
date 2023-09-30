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
	/// 게임 일시정지 상태인지 여부
	/// </summary>
	public bool IsGamePause { get; set; } = false;

	/// <summary>
	/// 매장 운영 중인지 확인
	/// </summary>
	public bool IsOpenrestaurant { get; set; } = false;
}
