using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredients : MonoBehaviour
{
	public IngrData None;
	public List<IngrData> ingrDatas;

	/// <summary>
	/// â�� �������� �ʱ� ��� ���� ����
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