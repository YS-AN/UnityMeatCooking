using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum IngredientType
{
	None,
	Meat,
	Garnish
}

public class IngrInfo
{
	public IngrInfo(IngrData data, int count = 0)
	{
		Data = data;
		Count = count;
	}

	public IngrData Data { get; set; }

	public int Count { get; set; }
}

[CreateAssetMenu(fileName = "IngrData", menuName = "Data/Ingredient")]
public class IngrData : ScriptableObject
{
	/// <summary>
	/// ��� �̸�
	/// </summary>
	public string Name;

	/// <summary>
	/// ��� ����
	/// </summary>
	public IngredientType IngrType;

	/// <summary>
	/// ���� �̸�
	/// </summary>
	private string folderName = "Storage";

	/// <summary>
	/// ���� ��ġ
	/// </summary>
	public string Path 
	{ 
		get 
		{
			if (IngrType == IngredientType.None)
				return string.Format("{0}/Empty", folderName);

			return string.Format("{0}/{1}/Image/{2}", folderName, IngrType, Name); 
		}
	}
}

