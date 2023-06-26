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

		//todo.���� ��� �� ui ȣ��

		StartCoroutine(tmpRoutine());
	}

	public void CookedFood()
	{
		cookInfo.BeingCookedFood.rotation = Quaternion.identity;

		//todo.��� (Ÿ�� ȿ���� �ʿ��� ��..!)
	}

	private IEnumerator tmpRoutine()
	{
		yield return new WaitForSeconds(5);

		CookedFood();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		//todo. player �տ� ���
	}
}
