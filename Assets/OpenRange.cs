using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenRange : MonoBehaviour
{
	[SerializeField]
	private LayerMask layerMask;

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("IsOpen?");

		if (layerMask.IsContain(other.gameObject.layer))
		{
			//Debug.Log("IsOpen?");
		}
	}

	private void OnTriggerExit(Collider other)
	{
		Debug.Log("BYE?");

		if (layerMask.IsContain(other.gameObject.layer))
		{
			//Debug.Log("BYE?");
		}
	}
}
