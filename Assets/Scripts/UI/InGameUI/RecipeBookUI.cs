using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RecipeBookUI : InGameUI
{
	public void SetRecipeBookUI(IngredientName foodType)
	{
		images["DrumstickRecipe"].gameObject.SetActive(foodType == IngredientName.Drumstick);
		images["GrilledFishRecipe"].gameObject.SetActive(foodType == IngredientName.GrilledFish);
		images["TBornSteakRecipe"].gameObject.SetActive(foodType == IngredientName.TBornSteak);
	}
}
