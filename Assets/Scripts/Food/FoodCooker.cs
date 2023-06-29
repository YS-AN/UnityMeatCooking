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
		//todo.조리 대기 중 ui 호출

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

		//todo.대기 (타는 효과가 필요할 듯..!)

		StartCoroutine(CookedRoutine());
	}

	
	private IEnumerator CookedRoutine(float duration = 5f)
	{
		Color startColor = renderer.material.color; //todo.이건 후라이펜 색이 바뀌니까... 음식  색이 바뀌돋록 수정 필요
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
		//renderer.material.color = Color.black; //서서히 음식 오브젝트거 검정색으로 바뀌도록 해야함
		//Destroy(gameObject);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		StopCookingRoutine();
		Destroy(gameObject);

		OnFinishedCook?.Invoke(coookingInfo);
	}
}
