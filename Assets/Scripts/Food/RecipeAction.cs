using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RecipeAction : MonoBehaviour, IActionable
{
	[SerializeField]
	private Transform recipes;

	public bool isEntredPlayer { get; set; } = false;

	public Transform StopPoint { get; set; }

	public UnityAction OnExitRecipeAction;

	private void Awake()
	{
		StopPoint = transform;
	}

	public void NextAction()
	{
		isEntredPlayer = true;
		recipes.gameObject.SetActive(true);
	}

	public void ClearAction()
	{
		isEntredPlayer = false;
		OnExitRecipeAction?.Invoke();

		recipes.gameObject.SetActive(false);
	}

}
