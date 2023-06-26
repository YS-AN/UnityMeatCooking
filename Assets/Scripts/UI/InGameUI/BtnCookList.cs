using JetBrains.Rider.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BtnCookList : MonoBehaviour
{
	Button button;

	public void InitButton(FoodData food)
	{
		button = transform.GetComponent<Button>();
		button.onClick.AddListener(() => { OpenInventory(food); });
	}

	private void OpenInventory(FoodData food)
	{
		var foodPrefab = GameManager.Resource.Load<Customer>(food.CookingObjectPath);

		var cooking = Instantiate(foodPrefab, food.CookingObject.transform.position, food.CookingObject.transform.rotation);
		cooking.AddComponent<Rigidbody>();
	}
}
