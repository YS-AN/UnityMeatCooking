using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ServedDishModel
{
	public Dish ServedDish;

	public OrderInfo HoldingFood;

	public Transform HoldingPoint { get; set; }

	public int HoldingAnimationKey { get; set; }
}

public class ChefCooker : MonoBehaviour
{
	[SerializeField]
	private LayerMask FoodLayer;

	[SerializeField]
	private List<Transform> HoldingPoints;
	private Dictionary<int, bool> HoldingAnimations;

	private List<ServedDishModel> ServedInfo;

	private Animator animator;
	private bool IsRunningAnim;

	public bool IsHoldable { get { return (ServedInfo != null && HoldingPoints.Count > 0); } }

	public bool IsHoldAnyFood { get { return (ServedInfo != null && ServedInfo.Count > 0); } }

	private void Awake()
	{
		animator = GetComponent<Animator>();
		IsRunningAnim = false;

		ServedInfo = new List<ServedDishModel>();

		HoldingAnimations = new Dictionary<int, bool>() { { 1, false }, { 2, false } };
	}

	/// <summary>
	/// 음식을 잡기
	/// </summary>
	/// <param name="orderData">잡을 음식</param>
	public void HoldDish(OrderInfo orderData)
	{
		ServedDishModel model = new ServedDishModel();
		model.HoldingFood = orderData;
		model.HoldingPoint = GetHoldingPoint();

		if(model.HoldingPoint != null)
		{
			model.ServedDish = GameManager.Resource.Instantiate<Dish>(orderData.FoodInfo.ResultObjectPath, model.HoldingPoint.position, model.HoldingPoint.rotation);
			model.ServedDish.transform.SetParent(model.HoldingPoint, true);
			model.ServedDish.OrderId = orderData.OderID;

			if (orderData.CookResultType == CookedType.Undercooked)
			{
				SetUndercookedFood(model.ServedDish);
			}
			ServedInfo.Add(model);

			model.HoldingAnimationKey = GetHoldingAnimationKey();
		}
	}

	/// <summary>
	/// 음식을 지정된 위치에 내려놓기
	/// </summary>
	/// <param name="putDonwPoint">음식 내려놓을 위치</param>
	/// <param name="orderID">내려놓을 음식 id</param>
	/// <returns></returns>
	public Dish PutDownDish(Transform putDonwPoint, string orderID)
	{
		var servedModel = GetHoldingFoodModel(orderID);

		bool isUndercooked = servedModel.HoldingFood.CookResultType == CookedType.Undercooked;
		string dishObjPath = servedModel.HoldingFood.FoodInfo.ResultObjectPath;

		if (servedModel != null)
		{
			RemoveHoldingFood(servedModel); //들고 있던 음식 제거

			//지정된 위치에 음식 생성
			Dish dish = GameManager.Resource.Instantiate<Dish>(dishObjPath, putDonwPoint.position, putDonwPoint.rotation);
			dish.transform.SetParent(putDonwPoint, true);

			if (isUndercooked)
				SetUndercookedFood(dish);

			return dish;
		}
		return null;
	}

	/// <summary>
	/// 잡았던 음식 제거
	/// </summary>
	/// <param name="servedModel"></param>
	public void RemoveHoldingFood(ServedDishModel servedModel)
	{
		Debug.Log("[ChefCooker] RemoveHoldingFood222");

		Destroy(servedModel.ServedDish.gameObject);
		ReturnHoldingPoint(servedModel.HoldingPoint, servedModel.HoldingAnimationKey);
		ServedInfo.Remove(servedModel);

		servedModel.HoldingFood.IsOrder = false;

		Debug.Log("[ChefCooker] HoldingFood.IsOrder = false;");

		if (IsRunningAnim)
			animator.SetBool("IsServe", GetAnimatorWorkValue());
	}

	public void RemoveHoldingFood(string orderId)
	{
		Debug.Log("[ChefCooker] RemoveHoldingFood");

		RemoveHoldingFood(GetHoldingFoodModel(orderId));
	}

	private ServedDishModel GetHoldingFoodModel(string orderId)
	{
		return ServedInfo.Where(x => x.HoldingFood.OderID == orderId).FirstOrDefault();
	}

	public OrderInfo CurrentHoldingFood(string orderId)
	{
		var model = GetHoldingFoodModel(orderId);
		return model == null ? null : model.HoldingFood;
	}

	private void SetUndercookedFood(Dish dish)
	{
		dish.CookedFood.transform.localPosition = new Vector3(0, 0.02f, 0);
		dish.CookedFood.transform.localRotation = Quaternion.Euler(0, 0, 180);
	}

	private bool GetAnimatorWorkValue()
	{
		IsRunningAnim = !(ServedInfo.Count == 0);
		return IsRunningAnim;
	}

	private Transform GetHoldingPoint()
	{
		if (HoldingPoints.Count > 0)
		{
			Transform point = HoldingPoints[0];
			HoldingPoints.RemoveAt(0);
			return point;
		}
		return null;
	}

	private int GetHoldingAnimationKey()
	{
		var animation = HoldingAnimations.Where(x => x.Value == false).FirstOrDefault();

		if(animation.Key > 0)
		{
			animator.SetLayerWeight(animation.Key, 1);
			HoldingAnimations[animation.Key] = true;

			return animation.Key;
		}
		return -1;
	}


	private void ReturnHoldingPoint(Transform point, int animationKey)
	{
		HoldingPoints.Add(point);

		HoldingAnimations[animationKey] = false;
		animator.SetLayerWeight(animationKey, 0);
	}
}
