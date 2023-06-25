using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderRange : MonoBehaviour
{
	private const string UI_PATH = "UI/Ordering";


	[SerializeField]
	private LayerMask PlayerMask;

	private OrderingUI ordering;

	/*
	private void OnTriggerEnter(Collider other)
	{
		if (PlayerMask.IsContain(other.gameObject.layer))
		{
			ordering = GameManager.UI.ShowInGameUI<OrderingUI>(UI_PATH);
			ordering.SetTarget(transform);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (PlayerMask.IsContain(other.gameObject.layer))
		{
			if (ordering != null)
			{
				GameManager.UI.CloseInGameUI(ordering);
			}
		}
	}
	//*/
}
