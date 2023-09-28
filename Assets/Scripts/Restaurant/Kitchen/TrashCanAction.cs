using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCanAction : MonoBehaviour, IActionable
{
	public Transform StopPoint { get; set; }

	[SerializeField]
	private TrashCan trashcan;

	private void Awake()
	{
		StopPoint = transform;
	}

	public void NextAction()
	{
		trashcan.ThrowAwayTrash();
	}

	public void ClearAction()
	{
		trashcan.AfterThrewAway();
	}
}
