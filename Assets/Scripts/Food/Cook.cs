using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

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

	private void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Physics.Raycast(ray, out hit);

			if(hit.collider != null)
			{
				Cooker.ClickedFood();
			}
		}
	}
}
