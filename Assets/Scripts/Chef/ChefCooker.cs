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

	public HoldPoint HoldingPoint { get; set; }
}

public class ChefCooker : MonoBehaviour
{
	[SerializeField]
	private LayerMask FoodLayer;

	[SerializeField]
	private List<HoldPoint> HoldingPoints;
	private Dictionary<PlayerHoldDirection, bool> HoldingAnimations;

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

		HoldingAnimations = new Dictionary<PlayerHoldDirection, bool>() { { PlayerHoldDirection.Right, false }, { PlayerHoldDirection.Left, false } };
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

		Debug.Log($"[HoldDish] {model.HoldingPoint}");

		if(model.HoldingPoint != null)
		{
			Transform holdingPointForm = model.HoldingPoint.gameObject.transform;

			model.ServedDish = GameManager.Resource.Instantiate<Dish>(orderData.FoodInfo.ResultObjectPath, holdingPointForm.position, holdingPointForm.rotation);
			model.ServedDish.transform.SetParent(holdingPointForm, true);
			model.ServedDish.OrderId = orderData.OderID;

			if (orderData.CookResultType == CookedType.Undercooked)
			{
				SetUndercookedFood(model.ServedDish);
			}
			ServedInfo.Add(model);

			SetHoldingAnimation(model.HoldingPoint.PlayerDirection);
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
		Destroy(servedModel.ServedDish.gameObject);
		ReturnHoldingPoint(servedModel.HoldingPoint);
		ServedInfo.Remove(servedModel);

		servedModel.HoldingFood.IsOrder = false;

		if (IsRunningAnim)
			animator.SetBool("IsServe", GetAnimatorWorkValue());
	}

	public void RemoveHoldingFood(string orderId)
	{
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

	private HoldPoint GetHoldingPoint()
	{
		if (HoldingPoints.Count > 0)
		{
			HoldPoint point = HoldingPoints[0];
			HoldingPoints.RemoveAt(0);
			return point;
		}
		return null;
	}

	private PlayerHoldDirection SetHoldingAnimation(PlayerHoldDirection direction)
	{
		Debug.Log($"[SetHoldingAnimation] direction : {direction}");

		var animation = HoldingAnimations.Where(x => x.Key == direction && x.Value == false).FirstOrDefault();

		if(animation.Key > 0)
		{
			Debug.Log($"[SetHoldingAnimation] animation.Key {animation.Key}");

			animator.SetLayerWeight((int)direction, 1);
			HoldingAnimations[animation.Key] = true;

			return animation.Key;
		}

		Debug.Log($"[SetHoldingAnimation] NONE");
		return PlayerHoldDirection.None;
	}


	private void ReturnHoldingPoint(HoldPoint point)
	{
		Debug.Log($"[ReturnHoldingPoint] {point.PlayerDirection} ");

		HoldingPoints.Add(point);

		HoldingAnimations[point.PlayerDirection] = false;
		animator.SetLayerWeight((int)point.PlayerDirection, 0);
	}
}
