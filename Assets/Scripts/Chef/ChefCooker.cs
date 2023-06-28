using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChefCooker : MonoBehaviour
{
	[SerializeField]
	private LayerMask FoodLayer;

	[SerializeField]
	private Transform HoldingPoint;

	public FoodData HoldingFood;
	private Dish holdingDish;
	private Animator animator;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	public void HoldDish(FoodData foodData)
	{
		HoldingFood = foodData;

		animator.SetBool("IsServe", true);	

		Dish dishPrefab = GameManager.Resource.Load<Dish>(foodData.ResultObjectPath);
		holdingDish = Instantiate(dishPrefab, HoldingPoint.position, HoldingPoint.rotation);
		holdingDish.transform.SetParent(HoldingPoint, true);

	}

	public bool PutDownDish(Transform putDonwPoint, FoodData foodData)
	{
		if(holdingDish != null)
		{
			animator.SetBool("IsServe", false);

			HoldingFood = null;
			Destroy(holdingDish.gameObject);

			Dish dishPrefab = GameManager.Resource.Load<Dish>(foodData.ResultObjectPath);
			var newDish = Instantiate(dishPrefab, putDonwPoint.position, putDonwPoint.rotation);
			newDish.transform.SetParent(putDonwPoint, true);

			return true;
		}

		return false;
	}
}
