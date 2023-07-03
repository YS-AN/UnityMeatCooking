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
	/// 재료 이름
	/// </summary>
	public string Name;

	/// <summary>
	/// 재료 종류
	/// </summary>
	public IngredientType IngrType;

	/// <summary>
	/// 재료 개수 
	/// </summary>
	public int IngrCnt;

	/// <summary>
	/// 음식 이미지
	/// </summary>
	public Sprite Icon;

	/// <summary>
	/// 폴더 이름
	/// </summary>
	private string folderName = "Storage";

	/// <summary>
	/// 파일 위치
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

