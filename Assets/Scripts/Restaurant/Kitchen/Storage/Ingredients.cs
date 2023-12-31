using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredients : MonoBehaviour
{
	public IngrData None;
	public List<IngrData> ingrDatas;

	/// <summary>
	/// 창고에 보유중인 초기 재료 개수 설정
	/// </summary>
	public void InitIngrHavingCount()
	{
		foreach (var ingr in ingrDatas)
		{
			if (ingr.Name == IngredientName.Pineapple)
				ingr.IngrCnt = 10;
			else if (ingr.IngrType == IngredientType.Garnish)
				ingr.IngrCnt = 5;
			else
				ingr.IngrCnt = 3;
		}
	}
}