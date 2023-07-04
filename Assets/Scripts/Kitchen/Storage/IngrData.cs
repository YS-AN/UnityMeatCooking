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
	/// ��� �̸�
	/// </summary>
	public IngredientName Name;

	/// <summary>
	/// ��� ����
	/// </summary>
	public IngredientType IngrType;

	/// <summary>
	/// ��� ���� 
	/// </summary>
	public int IngrCnt;

	/// <summary>
	/// ��� �̹���
	/// </summary>
	public Sprite Icon;

	/// <summary>
	/// ��� ����
	/// </summary>
	public int Price;
}

