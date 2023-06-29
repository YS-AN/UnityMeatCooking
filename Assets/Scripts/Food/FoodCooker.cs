using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class FoodCooker : MonoBehaviour, IPointerClickHandler
{
	private bool isfinished;

	public UnityAction<OrderInfo> OnCooking;
	public UnityAction<OrderInfo> OnFinishedCook;

	public Renderer renderer;

	private OrderInfo coookingInfo;

	private Coroutine cookRoutine;



	private void Awake()
	{
		isfinished = false;
		renderer = GetComponent<Renderer>();

		OnCooking += Cooking;
	}

	private void Cooking(OrderInfo orderData)
	{
		coookingInfo = orderData;
		coookingInfo.CookResultType = CookedType.Undercooked;
		//todo.���� ��� �� ui ȣ��

		cookRoutine = StartCoroutine(CookingRoutine());
	}


	private IEnumerator CookingRoutine()
	{
		yield return new WaitForSeconds(5);

		StopCookingRoutine();
		CookedFood();
	}

	private void StopCookingRoutine()
	{
		if (cookRoutine != null)
		{
			StopCoroutine(cookRoutine);
			cookRoutine = null;
		}
	}


	public void CookedFood()
	{
		isfinished = true;
		transform.GetComponent<Cook>().BeingCookedFood.rotation = Quaternion.identity;
		coookingInfo.CookResultType = CookedType.Perfect;

		//todo.��� (Ÿ�� ȿ���� �ʿ��� ��..!)

		StartCoroutine(CookedRoutine());
	}

	
	private IEnumerator CookedRoutine(float duration = 5f)
	{
		Color startColor = renderer.material.color; //todo.�̰� �Ķ����� ���� �ٲ�ϱ�... ����  ���� �ٲ�� ���� �ʿ�
		float time = 0f;

		while (time < 1f)
		{
			time += Time.deltaTime / duration;
			renderer.material.color = Color.Lerp(startColor, Color.black, time);
			yield return null;
		}

		BurnedFood();
	}

	private void BurnedFood()
	{
		coookingInfo.CookResultType = CookedType.Overcooked;
		//renderer.material.color = Color.black; //������ ���� ������Ʈ�� ���������� �ٲ�� �ؾ���
		//Destroy(gameObject);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		StopCookingRoutine();
		Destroy(gameObject);

		OnFinishedCook?.Invoke(coookingInfo);
	}
}
