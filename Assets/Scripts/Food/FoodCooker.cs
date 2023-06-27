using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class FoodCooker : MonoBehaviour, IPointerClickHandler
{
	private bool isfinished;

	public UnityAction OnCooking;

	private void Awake()
	{
		isfinished = false;

		OnCooking += Cooking;
	}

	private void Cooking()
	{
		//todo.조리 대기 중 ui 호출

		StartCoroutine(tmpCookingRoutine());
	}

	public void CookedFood()
	{
		isfinished = true;
		transform.GetComponent<Cook>().BeingCookedFood.rotation = Quaternion.identity;

		//todo.대기 (타는 효과가 필요할 듯..!)


		StartCoroutine(tmpCookedRoutine());
	}

	private IEnumerator tmpCookingRoutine()
	{
		yield return new WaitForSeconds(5);

		CookedFood();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		StopCoroutine(tmpCookingRoutine());
		//OnFinishedCooking?.Invoke();
	}

	private IEnumerator tmpCookedRoutine()
	{
		yield return new WaitForSeconds(5);

		BurnedFood();
	}

	private void BurnedFood()
	{
		Destroy(gameObject);
	}
}
