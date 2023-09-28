using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageAction : MonoBehaviour, IActionable
{
	[SerializeField]
	private Storage storage;

	public Transform StopPoint { get; set; }

	private void Awake()
	{
		StopPoint = transform;
	}

	public void NextAction()
	{
		storage.OpenStorageUI();
	}

	public void ClearAction()
	{
		storage.ClearStorageUI();
	}
}
