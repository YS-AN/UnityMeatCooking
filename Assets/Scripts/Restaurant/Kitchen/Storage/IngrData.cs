using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum IngredientType
{
	None,
	Meat,
	Garnish
}

public enum IngredientName
{
	None = -1,
	Drumstick = 0,
	TBornSteak,
	ChiliPepper,
	Tomato,


	test_Tomato1,
	test_Tomato2,
	test_Tomato3,
		
	test_Tomato4,
	test_Tomato5,
	test_Tomato6,
	test_Tomato7,
		
	test_Tomato8,
	test_Tomato9,
	test_Tomato10,
}

public class IngrInfo
{
	public IngrInfo(IngrData data)
	{
		Data = data;
	}

	public IngrData Data { get; set; }

	public int Count
	{
		get { return Data.IngrCnt; }
		set
		{
			Data.IngrCnt = value;
			OnChangedCnt?.Invoke(Data.IngrCnt); 
		}
	}

	public UnityAction<int> OnChangedCnt;

}


public class SaleItemData : ScriptableObject
{
	/// <summary>
	/// 재료 이미지
	/// </summary>
	public Sprite Icon;

	/// <summary>
	/// 재료 가격
	/// </summary>
	public int Price;
}

[CreateAssetMenu(fileName = "IngrData", menuName = "Data/Ingredient")]
public class IngrData : SaleItemData
{
	/// <summary>
	/// 재료 이름
	/// </summary>
	public IngredientName Name;

	/// <summary>
	/// 재료 종류
	/// </summary>
	public IngredientType IngrType;

	/// <summary>
	/// 재료 개수 
	/// </summary>
	public int IngrCnt;
}

