using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearthOvenAction : MonoBehaviour, IActionable
{
	public Transform StopPoint { get; set; }

	[SerializeField]
	private HearthOven oven;

	private void Awake()
	{
		StopPoint = transform;
	}

	public void NextAction()
	{
		oven.OpenCookListUI();
	}

	public void ClearAction()
	{
		oven.CloseCookListUI();
	}

}
