using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recipe
{
	Button btnCook;
}

public class CookListUI : InGameUI
{
	protected override void Awake()
	{
		base.Awake();

	}

	public void SetCookList(List<Recipe> list)
	{
		foreach(Recipe recipe in list)
		{

		}
	}
}
