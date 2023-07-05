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
		foreach(var ingr in ingrDatas)
		{
			ingr.IngrCnt = 10;
		}

		foreach(var ingr in testData)
		{
			ingrDatas.Add(ingr);
		}
	}
}