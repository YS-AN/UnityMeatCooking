using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public interface ICancelableOrder : IObservable
{
}

public class FoodManager : MonoBehaviour //: NotifyContorller<ICancelableOrder>
{
	private static FoodManager instance;

	public static FoodManager GetInstance()
	{
		if (instance == null)
			instance = new FoodManager();

		return instance;
	}

	private Dictionary<int, FoodData> _foodDic;
	public Dictionary<int, FoodData> FoodDic { get { return _foodDic; } }

	private Dictionary<int, FoodData> _canCookDic;
	public Dictionary<int, FoodData> CanCookDic { get { return _foodDic; } }

	private Dictionary<int, OrderInfo> _orderList;

	public List<int> CanCookIndex { get { return CanCookDic.Keys.ToList(); } }

	public Transform CookPos;

	//protected override void Awake()
	//{
	//	base.Awake();
	private void Awake()
	{
		instance = this;
		SetFoodDic();
	}

	private void Start()
	{
		
	}

	private void SetFoodDic()
	{
		_foodDic = new Dictionary<int, FoodData>();
		_orderList = new Dictionary<int, OrderInfo>();

		Menu menu = transform.GetComponent<Menu>();

		int index = 0;
		foreach(var food in menu.CookList)
		{
			_foodDic.Add(index++, food);
		}

		SetPossibleCook();
	}

	/// <summary>
	/// 전체 레시피에서 제조 가능한 음식 자겨오기
	/// </summary>
	private void SetPossibleCook()
	{
		_canCookDic = _foodDic.Where(x => x.Value.IsLearn).ToDictionary(key => key.Key, val => val.Value);
	}

	/// <summary>
	/// 새로운 레시피 배우기
	/// </summary>
	/// <param name="index">제조 가능한 음식 추가하기</param>
	public void LearnCook(int index)
	{
		if(_foodDic.ContainsKey(index))
		{
			_foodDic[index].IsLearn = true;
			_canCookDic.Add(index, _foodDic[index]);
		}
	}

	/// <summary>
	/// 주문 내역 추가
	/// </summary>
	/// <param name="orderData">추가할 주문 내역</param>
	public void AddOrder(OrderInfo orderData)
	{
		int key = (_orderList.Keys.Count > 0 ? _orderList.Keys.Max() : 0) + 1;
		_orderList.Add(key, orderData);
	}

	/// <summary>
	/// 주문 내역 삭제
	/// </summary>
	/// <param name="index">삭제할 인덱스</param>
	public void RemoveOrder(int index)
	{
		_orderList.Remove(index);
	}

	/// <summary>
	/// 주문 내역 전체 삭제
	/// </summary>
	public void RemoveAllOrder()
	{
		_orderList.Clear();
	}

	/// <summary>
	/// 주문 내역 반환
	/// </summary>
	/// <returns></returns>
	public Dictionary<int, OrderInfo> GetOrderList()
	{
		return _orderList.Where(x => x.Value != null).ToDictionary(k => k.Key, v => v.Value);
	}

	
}
