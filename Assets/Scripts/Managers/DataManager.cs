using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DataManager : MonoBehaviour
{
	private int revenue;
	public int Revenue { get { return revenue; } set { revenue = value; OnChangeRevenue?.Invoke(); } }
	public string RevenueToStr { get { return string.Format("{0:0,0}", revenue); } }
	public event UnityAction OnChangeRevenue;

	/// <summary>
	/// 매장 꾸미기 화면인지 여부
	/// </summary>
	public bool IsPlaceable { get; set; }

	/// <summary>
	/// 게임 일시정지 상태인지 여부
	/// </summary>
	public bool IsGamePause { get; set; }
}
