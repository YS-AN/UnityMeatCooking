using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredients : MonoBehaviour
{
	public IngrData None;
	public List<IngrData> ingrDatas;

	public List<IngrData> testData;

	private void Awake()
	{
		foreach (var ingr in testData)
		{
			ingrDatas.Add(ingr);
		}
	}

	/// <summary>
	/// 창고에 보유중인 초기 재료 개수 설정
	/// </summary>
	public void InitIngrHavingCount()
	{
		foreach (var ingr in ingrDatas)
		{
			ingr.IngrCnt = 10;
		}
	}
}