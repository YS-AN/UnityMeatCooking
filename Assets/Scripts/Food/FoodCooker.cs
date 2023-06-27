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
		//todo.���� ��� �� ui ȣ��

		StartCoroutine(tmpCookingRoutine());
	}

	public void CookedFood()
	{
		isfinished = true;
		transform.GetComponent<Cook>().BeingCookedFood.rotation = Quaternion.identity;

		//todo.��� (Ÿ�� ȿ���� �ʿ��� ��..!)


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
