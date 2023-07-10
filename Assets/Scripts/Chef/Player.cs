using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public ChefMover Mover;
	public ChefCooker Cooker;
	public ChefCleaner Cleaner;
	public TPSCameraController Camera; 

	public Animator Animator;

	private void Awake()
	{
		Animator = GetComponent<Animator>();

		Mover = GetComponent<ChefMover>();
		Cooker = transform.GetComponent<ChefCooker>();
		Cleaner = transform.GetComponent<ChefCleaner>();
		Camera = transform.GetComponent<TPSCameraController>();
	}
}
