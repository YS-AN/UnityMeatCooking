using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


//��ư�� �ڽſ� �°� �ڵ������� �� �ִ� ����� ������?

public class CookListUI : InGameUI
{
	private int Max = 16;

	public FoodData foodData;

	[SerializeField]
	private Transform BtnContent; 

	private BtnCook[] cookList;
	private int curIndex;

	protected override void Awake()
	{
		base.Awake();

		//cookList = BtnContent.GetComponentsInChildren<BtnCook>();
		curIndex = 0;
	}

	public bool AddNewFood(BtnCook newCook)
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
}
