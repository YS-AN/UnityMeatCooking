using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoration : MonoBehaviour
{
	private CinemachineVirtualCamera MainCam;

    public void DecorateRestaurant()
    {
		MainCam = Camera.main.GetComponent<CinemachineVirtualCamera>();
        MainCam.Priority = 10;
	}

    private void EndOfDecoration()
    {
        MainCam.Priority = 30;
	}
}
