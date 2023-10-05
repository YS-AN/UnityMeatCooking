using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RecipeImgInfo : MonoBehaviour, IPointerClickHandler
{
	private const string UI_PATH = "UI/RecipeSource";

	[SerializeField]
	private IngredientName recipeType;

	[SerializeField]
	private RecipeAction stopPoint;

	private RecipeBookUI recipeBook;

	private void Awake()
	{
		stopPoint.OnExitRecipeAction += ClearRecipeUI;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (stopPoint.isEntredPlayer && recipeBook == null)
		{
			recipeBook = GameManager.UI.ShowInGameUI<RecipeBookUI>(UI_PATH);
			recipeBook.SetTarget(transform);
			recipeBook.SetRecipeBookUI(recipeType);
		}
		else
			ClearRecipeUI();
	}

	private void ClearRecipeUI()
	{
		if (recipeBook != null)
		{
			GameManager.UI.CloseInGameUI(recipeBook);
			recipeBook = null;
		}
	}
}
