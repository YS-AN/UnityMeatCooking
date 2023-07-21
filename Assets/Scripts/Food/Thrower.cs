using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Thrower : MonoBehaviour, IPointerClickHandler
{
	public void OnPointerClick(PointerEventData eventData)
	{
		Debug.Log("[Thrower] OnPointerClick");

		Player palyer = PlayerManager.GetInstance().Player;

		if (palyer.Cooker.IsHoldAnyFood)
		{
			Debug.Log("[Thrower] IsHoldAnyFood");

			if (palyer.Cleaner.IsUseTrashCan)
			{
				ThrowAwayDish();
			}
		}
	}

	private void ThrowAwayDish()
	{
		Debug.Log("[Thrower] ThrowAwayDish");

		Dish dish = transform.GetComponent<Dish>();
		PlayerManager.GetInstance().Player.Cooker.RemoveHoldingFood(dish.OrderId);
	}
}
