using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Cook : MonoBehaviour
{
	[SerializeField]
	private Transform beingCookedFood;
	public Transform BeingCookedFood { get { return beingCookedFood; } }

	public FoodCooker Cooker { get; private set; }

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
