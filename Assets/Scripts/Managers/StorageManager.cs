using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageManager : MonoBehaviour
{
	private const string UI_PATH = "UI/OpenStorage";

	private static StorageManager instance;

	public static StorageManager GetInstance()
	{
		if (instance == null)
			instance = new StorageManager();

		return instance;
	}

	private Dictionary<int, IngrInfo> _ingredients;
	public Dictionary<int, IngrInfo> Ingredients { get { return _ingredients; } }

	private void Awake()
	{
		instance = this;

		SetDictionary();
	}

	private void SetDictionary()
	{
		_ingredients = new Dictionary<int, IngrInfo>();

		Ingredients ingredients = transform.GetComponent<Ingredients>();

		int index = 0;
		foreach (var data in ingredients.ingrDatas)
		{
			_ingredients.Add(index++, new(data));
		}
	}
}
