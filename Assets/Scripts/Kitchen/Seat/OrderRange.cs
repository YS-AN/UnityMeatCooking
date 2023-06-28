using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrderRange : MonoBehaviour
{
	private const string UI_PATH = "UI/Ordering";

	[SerializeField]
	private LayerMask PlayerMask;

	[SerializeField]
	private Chair SeatChair;

	private OrderingUI ordering;
	

	private void Awake()
	{
		SeatChair = transform.GetComponentInParent<Chair>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (PlayerMask.IsContain(other.gameObject.layer))
		{
			FoodData holdingFood = PlayerManager.GetInstance().Player.Cooker.HoldingFood;

			if(holdingFood != null)
			{
				var orderedCust = SeatChair.EntryCusts.Where(x => x.Order.OrderFood.FoodInfo.Name == holdingFood.name).FirstOrDefault();

				if (orderedCust != null)
				{
					orderedCust.Eater.OnStateAction?.Invoke(orderedCust, CustStateType.Eating);
				}

			}
		}
	}
}
