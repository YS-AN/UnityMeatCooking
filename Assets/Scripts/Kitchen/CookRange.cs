using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookRange : MonoBehaviour
{
	[SerializeField]
	private LayerMask PlayerMask;

	[SerializeField]
	private Transform StorageDoor;

	private OpenStorage openStorage;

	private void OnTriggerEnter(Collider other)
	{
		
	}

	private void OnTriggerExit(Collider other)
	{
		
	}
}
