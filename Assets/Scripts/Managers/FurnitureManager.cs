using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FurnitureManager : MonoBehaviour
{
	private static FurnitureManager instance;

	public static FurnitureManager GetInstance()
	{
		if (instance == null)
			instance = new FurnitureManager();

		return instance;
	}

	private Dictionary<FurnitureName, FurnData> _furnitures;
	public Dictionary<FurnitureName, FurnData> Furnitures { get { return _furnitures; } }

	private void Awake()
	{
		instance = this;

		SetDictionary();
	}

	private void SetDictionary()
	{
		_furnitures = new Dictionary<FurnitureName, FurnData>();
		Furnitures furnitures = transform.GetComponent<Furnitures>();

		foreach (var data in furnitures.furnDatas)
		{
			_furnitures.Add(data.Name, data);
		}
	}
}
