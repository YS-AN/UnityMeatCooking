using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

//버튼을 자신에 맞게 자동생성할 수 있는 방법이 없으띾?

public class CookListUI : InGameUI
{
	private int Max = 16;

	public FoodData foodData;

	[SerializeField]
	private Transform BtnContent; 

	private Button[] cookList;
	private int curIndex;

	public delegate bool OrderDelegate(BtnCook newCook);
	public event OrderDelegate OnAddOrder;

	protected override void Awake()
	{
		base.Awake();
		
		InitButton();
	}

	private void OnEnable()
	{
		SetBtnContent();
	}



	private void InitButton()
	{
		cookList = BtnContent.GetComponentsInChildren<Button>();

		foreach(var btn in cookList)
		{
			btn.transform.AddComponent<Orderable>();
		}
	}


	/*
	private bool AddNewFood(BtnCook newCook)
	{
		if (curIndex <= Max)
		{
			newCook.FoodIndex = curIndex;
			cookList[curIndex++] = newCook;
			return true;
		}
		return false;
	}

	public BtnCook GetFood(int index)
	{
		if (index > curIndex)
			return null;

		return cookList[index];	
	}
	//*/

	private void SetBtnContent()
	{
		var orderList = FoodManager.GetInstance().GetOrderList();

		int index = 0;
		foreach (var food in orderList)
		{
			cookList[index++].image.sprite = food.Value.Icon;

			var order = cookList[index++].transform.GetComponent<Orderable>();
			order.OderID = food.Key;
			order.FoodInfo = food.Value;
		}
	}

}
