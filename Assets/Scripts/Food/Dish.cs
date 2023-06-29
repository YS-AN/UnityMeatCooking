using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dish : MonoBehaviour
{
	[SerializeField]
	private Transform cookedFood;
	public Transform CookedFood { get { return cookedFood; } }
}
