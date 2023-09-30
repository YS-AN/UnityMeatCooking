using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
	private int currentFloor = 0;

	[SerializeField]
	private FloorStep[] extensionFloors;

	public void ExtensionFloor()
	{
		if(currentFloor < extensionFloors.Length)
		{
			extensionFloors[currentFloor++].InstallNextFloor();
			extensionFloors[currentFloor].InstallFloor();
		}
		
	}
}
