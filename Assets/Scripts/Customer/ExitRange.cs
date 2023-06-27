using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitRange : MonoBehaviour
{
	[SerializeField]
	private LayerMask CustLayer;

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("ExitRange - OnTriggerEnter");

		if (CustLayer.IsContain(other.gameObject.layer))
		{
			Customer customer = other.gameObject.GetComponent<Customer>();
			if (customer != null)
			{
				customer.Mover.OnRemove?.Invoke();
			}
		}
	}
}
