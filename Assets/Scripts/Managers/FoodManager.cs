using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
	private static FoodManager instance;

	public static FoodManager GetInstance()
	{
		if (instance == null)
			instance = new FoodManager();

		return instance;
	}

	private Dictionary<int, FoodData> _foodDic;
	public Dictionary<int, FoodData> FoodDic { get { return _foodDic; } }

	private Dictionary<int, FoodData> _canCookDic;
	public Dictionary<int, FoodData> CanCookDic { get { return _foodDic; } }

	public List<int> CanCookIndex { get { return CanCookDic.Keys.ToList(); } }

	private void Awake()
	{
		instance = this;

		SetFoodDic();
	}

	private void SetFoodDic()
	{
		_foodDic = new Dictionary<int, FoodData>();

		Foods foods = GetComponent<Foods>();

		int index = 0;
		foreach(var food in foods.CookList)
		{
			_foodDic.Add(index++, food);
		}

		SetPossibleCook();
	}

	private void SetPossibleCook()
	{
		_canCookDic = _foodDic.Where(x => x.Value.IsLearn).ToDictionary(key => key.Key, val => val.Value);
	}

	public void LearnCook(int index)
	{
		if(_foodDic.ContainsKey(index))
		{
			_foodDic[index].IsLearn = true;
			_canCookDic.Add(index, _foodDic[index]);
		}
	}
}
