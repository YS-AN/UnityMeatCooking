using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public Animator Animator { get; private set; }
	public ChefMover Mover { get; private set; }
	public ChefCooker Cooker { get; private set; }
	public ChefCleaner Cleaner { get; private set; }
	
	private void Awake()
	{
		Animator = GetComponent<Animator>();
		Mover = GetComponent<ChefMover>();
		Cooker = transform.GetComponent<ChefCooker>();
		Cleaner = transform.GetComponent<ChefCleaner>();
	}
}
