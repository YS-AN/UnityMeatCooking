using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public bool IsInTrigger;

	public ChefMover Mover;
	public ChefCooker Cooker;
	public ChefCleaner Cleaner;
	public TPSCameraController Camera; 

	private void Awake()
	{
		Mover = GetComponent<ChefMover>();
		Cooker = transform.GetComponent<ChefCooker>();
		Cleaner = transform.GetComponent<ChefCleaner>();
		Camera = transform.GetComponent<TPSCameraController>();
	}
}
