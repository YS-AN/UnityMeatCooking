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


	private void Awake()
	{
		SeatChair = transform.GetComponentInParent<Chair>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (PlayerMask.IsContain(other.gameObject.layer))
		{
			PlayerManager.GetInstance().Player.IsInTrigger = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (PlayerMask.IsContain(other.gameObject.layer))
		{
			PlayerManager.GetInstance().Player.IsInTrigger = false;
		}
	}
}
