using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CounterRange : MonoBehaviour
{
	public LayerMask CookerMask;

	public UnityEvent<PlayerMover> OnInRangeCooker;
	public UnityEvent<PlayerMover> OnOutRangeCooker;

	private void OnTriggerEnter(Collider other)
	{
		
	}

	private void OnTriggerExit(Collider other)
	{
		
	}
}
