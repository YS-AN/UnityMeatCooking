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

