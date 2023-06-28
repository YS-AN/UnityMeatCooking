using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public enum CookedType
{
	None,
	Undercooked,
	Perfect,
	Overcooked
}

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
