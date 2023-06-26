using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class FoodCooker : MonoBehaviour, IPointerClickHandler
{
	private Cook cookInfo;

	public UnityAction<Cook> OnCooking;

	private void Awake()
	{
		OnCooking += Cooking;
	}

	private void Cooking(Cook cook)
	{
		cookInfo = cook;

		//todo.조리 대기 중 ui 호출

		StartCoroutine(tmpRoutine());
	}

	public void CookedFood()
	{
		cookInfo.BeingCookedFood.rotation = Quaternion.identity;

		//todo.대기 (타는 효과가 필요할 듯..!)
	}

	private IEnumerator tmpRoutine()
	{
		yield return new WaitForSeconds(5);

		CookedFood();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		//todo. player 손에 들기
	}
}
