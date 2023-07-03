using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class HavingIngrInfo
{
	public HavingIngrInfo(int count, Sprite sprite)
	{
		Count = count;
		IngrImg = sprite;
	}

	public int Count;
	public Sprite IngrImg;
}


public class StorageManager : MonoBehaviour
{
	private static StorageManager instance;

	public static StorageManager GetInstance()
	{
		if (instance == null)
			instance = new StorageManager();

		return instance;
	}

	public IngrData EmptyIngrData { get; private set; }

	private Dictionary<int, IngrInfo> _ingredients;
	public Dictionary<int, IngrInfo> Ingredients { get { return _ingredients; } }

	public Dictionary<int, HavingIngrInfo> HavingList;

	public UnityAction<int> OnSelectIngr;
	public UnityAction<int> OnDeselectIngr;

	private void Awake()
	{
		instance = this;

		SetDictionary();
	}

	private void SetDictionary()
	{
		_ingredients = new Dictionary<int, IngrInfo>();
		HavingList = new Dictionary<int, HavingIngrInfo>();

		Ingredients ingredients = transform.GetComponent<Ingredients>();

		int index = 0;
		foreach (var data in ingredients.ingrDatas)
		{
			_ingredients.Add(index++, new IngrInfo(data));
		}
		EmptyIngrData = ingredients.None;
	}

	public Dictionary<int, IngrInfo> GetInventoryList(IngredientType type)
	{
		var retVales = _ingredients.Where(x => x.Value.Count > 0);

		if (type == IngredientType.None)
			return retVales.ToDictionary(k => k.Key, v => v.Value);
		else
			return retVales.Where(x => x.Value.Data.IngrType == type).ToDictionary(k => k.Key, v => v.Value);
	}
}
