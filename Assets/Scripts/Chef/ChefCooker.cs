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
}

public class ChefCooker : MonoBehaviour
{
	[SerializeField]
	private LayerMask FoodLayer;

	[SerializeField]
	private List<Transform> HoldingPoints;

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
	}

	/// <summary>
	/// ������ ���
	/// </summary>
	/// <param name="orderData">���� ����</param>
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

			if(IsRunningAnim == false) 
				animator.SetBool("IsServe", GetAnimatorWorkValue());
		}
	}

	/// <summary>
	/// ������ ������ ��ġ�� ��������
	/// </summary>
	/// <param name="putDonwPoint">���� �������� ��ġ</param>
	/// <param name="orderID">�������� ���� id</param>
	/// <returns></returns>
	public Dish PutDownDish(Transform putDonwPoint, string orderID)
	{
		var servedModel = GetHoldingFoodModel(orderID);

		bool isUndercooked = servedModel.HoldingFood.CookResultType == CookedType.Undercooked;
		string dishObjPath = servedModel.HoldingFood.FoodInfo.ResultObjectPath;

		if (servedModel != null)
		{
			RemoveHoldingFood(servedModel); //��� �ִ� ���� ����

			//������ ��ġ�� ���� ����
			Dish dish = GameManager.Resource.Instantiate<Dish>(dishObjPath, putDonwPoint.position, putDonwPoint.rotation);
			dish.transform.SetParent(putDonwPoint, true);

			if (isUndercooked)
				SetUndercookedFood(dish);

			return dish;
		}
		return null;
	}

	/// <summary>
	/// ��Ҵ� ���� ����
	/// </summary>
	/// <param name="servedModel"></param>
	public void RemoveHoldingFood(ServedDishModel servedModel)
	{
		Destroy(servedModel.ServedDish.gameObject);
		ReturnHoldingPoint(servedModel.HoldingPoint);
		ServedInfo.Remove(servedModel);

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

	private void ReturnHoldingPoint(Transform point)
	{
		HoldingPoints.Add(point);
	}
}
