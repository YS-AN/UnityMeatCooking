using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class OrderRange : MonoBehaviour
{
	private const string UI_PATH = "UI/Ordering";

	[SerializeField]
	private LayerMask PlayerMask;

	[SerializeField]
	private Chair SeatChair;

	private OrderingUI ordering;

	public bool IsInTrigger { get; private set; }


	private void Awake()
	{
		SeatChair = transform.GetComponentInParent<Chair>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (PlayerMask.IsContain(other.gameObject.layer))
		{
			IsInTrigger = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (PlayerMask.IsContain(other.gameObject.layer))
		{
			IsInTrigger = false;
		}
	}
}
