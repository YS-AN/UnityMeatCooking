using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Decoration : SceneUI
{
	private CinemachineVirtualCamera MainCam;

	public UnityAction OnStartDecoration;
	public UnityAction OnEndDecoration;

	protected override void Awake()
	{
		base.Awake();

		buttons["BtnClose"].onClick.AddListener(() => { EndOfDecoration(); });

		OnStartDecoration += DecorateRestaurant;

		transform.gameObject.SetActive(false);
	}

	public void DecorateRestaurant()
    {
		MainCam = CinemachineCore.Instance.GetActiveBrain(0).ActiveVirtualCamera as CinemachineVirtualCamera;

		if (MainCam != null)
			MainCam.Priority = 10;

		transform.gameObject.SetActive(true);
	}

    private void EndOfDecoration()
    {
		transform.gameObject.SetActive(false);

		MainCam.Priority = 30;
		
		OnEndDecoration?.Invoke();
	}
}
