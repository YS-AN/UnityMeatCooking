using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

//요리 중
//시간 만큼 대기하다가 완성된 요리로 변경 (로테이션 변경)
//요리를 클릭하면, Dish로 바뀌고..

public class Cook : MonoBehaviour
{
	[SerializeField]
	private Transform beingCookedFood;
	public Transform BeingCookedFood { get { return beingCookedFood; } }

	public FoodCooker Cooker;

	private void Awake()
	{
		InitComponent();
	}

	private void InitComponent()
	{
		this.AddComponent<FoodCooker>();
		Cooker = GetComponent<FoodCooker>();
	}
}
