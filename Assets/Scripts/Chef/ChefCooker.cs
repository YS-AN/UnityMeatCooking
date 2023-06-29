using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChefCooker : MonoBehaviour
{
	[SerializeField]
	private LayerMask FoodLayer;

	[SerializeField]
	private Transform HoldingPoint;

	public OrderInfo HoldingFood;
	private Dish ServedDish;
	private Animator animator;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	public void HoldDish(OrderInfo orderData)
	{
		HoldingFood = orderData;

		animator.SetBool("IsServe", true);

		ServedDish = GameManager.Resource.Instantiate<Dish>(orderData.FoodInfo.ResultObjectPath, HoldingPoint.position, HoldingPoint.rotation);
		ServedDish.transform.SetParent(HoldingPoint, true);

		if(orderData.CookResultType == CookedType.Undercooked)
		{
			SetUndercookedFood(ServedDish);
		}
	}

	public Dish PutDownDish(Transform putDonwPoint)
	{
		if(ServedDish != null)
		{
			animator.SetBool("IsServe", false);

			bool isUndercooked = HoldingFood.CookResultType == CookedType.Undercooked;
			string dishObjPath = HoldingFood.FoodInfo.ResultObjectPath;

			HoldingFood = null;
			Destroy(ServedDish.gameObject);

			Dish dish = GameManager.Resource.Instantiate<Dish>(dishObjPath, putDonwPoint.position, putDonwPoint.rotation);
			dish.transform.SetParent(putDonwPoint, true);

			if (isUndercooked)
				SetUndercookedFood(dish);

			return dish;
		}
		return null;
	}

	private void SetUndercookedFood(Dish dish)
	{
		dish.CookedFood.transform.localPosition = new Vector3(0, 0.02f, 0);
		dish.CookedFood.transform.localRotation = Quaternion.Euler(0, 0, 180);
	}
}
