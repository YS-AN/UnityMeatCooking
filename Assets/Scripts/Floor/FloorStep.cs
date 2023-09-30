using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorStep : MonoBehaviour
{
	[SerializeField]
	private BoxCollider movingBorder;

	private void Awake()
	{
		movingBorder = gameObject.GetComponent<BoxCollider>();
	}

	public void InstallFloor()
	{
		transform.position = Vector3.zero;
	}

	public void InstallNextFloor()
	{
		movingBorder.gameObject.SetActive(false);
	}
}
