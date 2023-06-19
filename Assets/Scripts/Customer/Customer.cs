using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
	public const string ResourcesPath = "Prefabs/Cust_M1";

	public CustomerMover mover;

	private void Awake()
	{
		mover = GetComponent<CustomerMover>();
	}
}
