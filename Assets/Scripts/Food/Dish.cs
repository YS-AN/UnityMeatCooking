using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Dish : MonoBehaviour
{
	[SerializeField]
	private Transform cookedFood;
	public Transform CookedFood { get { return cookedFood; } }

	public string OrderId;

	public Thrower DishThrower;

	private void Awake()
	{
		InitComponent();
	}

	private void InitComponent()
	{
		this.AddComponent<Thrower>();
		DishThrower = GetComponent<Thrower>();
	}
}
