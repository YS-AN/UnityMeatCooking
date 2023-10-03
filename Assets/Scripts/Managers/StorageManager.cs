using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Events;

public class HavingIngrInfo
{
	public HavingIngrInfo(int count, Sprite sprite)
	{
		Count = count;
		IngrImg = sprite;
	}

	private int _count;
	public int Count { get { return _count; } set { _count = value; } }
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

	private Dictionary<IngredientName, IngrInfo> _ingredients;
	public Dictionary<IngredientName, IngrInfo> Ingredients { get { return _ingredients; } }

	public Dictionary<IngredientName, HavingIngrInfo> HavingList;

	public UnityAction<IngredientName> OnSelectIngr;
	public UnityAction<IngredientName> OnDeselectIngr;

	private void Awake()
	{
		instance = this;

		SetDictionary();
	}

	private void SetDictionary()
	{
		_ingredients = new Dictionary<IngredientName, IngrInfo>();
		HavingList = new Dictionary<IngredientName, HavingIngrInfo>();

		Ingredients ingredients = transform.GetComponent<Ingredients>();
		ingredients.InitIngrHavingCount();

		foreach (var data in ingredients.ingrDatas)
		{
			_ingredients.Add(data.Name, new IngrInfo(data));
		}
		EmptyIngrData = ingredients.None;
	}

	public Dictionary<IngredientName, IngrInfo> GetInventoryList(IngredientType type)
	{
		var retVales = _ingredients.Where(x => x.Value.Count > 0);

		if (type == IngredientType.None)
			return retVales.ToDictionary(k => k.Key, v => v.Value);
		else
			return retVales.Where(x => x.Value.Data.IngrType == type).ToDictionary(k => k.Key, v => v.Value);
	}
}
