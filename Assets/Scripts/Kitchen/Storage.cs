using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Storage : MonoBehaviour
{
	[SerializeField]
	private Transform Door;

	public UnityAction OnOpenDoor;

	private void Awake()
	{
		OnOpenDoor += OpenDoor;
	}

	private void OpenDoor()
	{
		Door.eulerAngles = new Vector3(0, 90, 0);
	}
}
