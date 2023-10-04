using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorStep : MonoBehaviour
{
	[SerializeField]
	private Transform movingBorder;

	public void InstallFloor()
	{
		transform.position = Vector3.zero;
	}

	public void InstallNextFloor()
	{
		SetMovingBorderActive(false);
	}

	public void SetMovingBorderActive(bool isActive)
	{
		movingBorder.gameObject.SetActive(isActive);
	}
}
