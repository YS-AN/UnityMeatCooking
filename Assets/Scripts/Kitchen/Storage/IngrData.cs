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
	Tomato
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

[CreateAssetMenu(fileName = "IngrData", menuName = "Data/Ingredient")]
public class IngrData : ScriptableObject
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

	/// <summary>
	/// 재료 이미지
	/// </summary>
	public Sprite Icon;

	/// <summary>
	/// 재료 가격
	/// </summary>
	public int Price;
}

