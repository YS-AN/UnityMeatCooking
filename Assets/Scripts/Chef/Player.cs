using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public bool IsInTrigger;

	public ChefCooker Cooker;
	public ChefCleaner Cleaner;

	private void Awake()
	{
		Cooker = transform.GetComponent<ChefCooker>();
		Cleaner = transform.GetComponent<ChefCleaner>();
	}
}
