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
	public string Name;

	/// <summary>
	/// ��� ����
	/// </summary>
	public IngredientType IngrType;

	/// <summary>
	/// ��� ���� 
	/// </summary>
	public int IngrCnt;

	/// <summary>
	/// ���� �̹���
	/// </summary>
	public Sprite Icon;

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

