using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour, IMoveable
{
	[SerializeField]
	private Transform trashCanCover;

	[SerializeField]
	private Transform stopPosition;

	public Vector3 StopPoint { get; set; }

	private CinemachineVirtualCamera cam2; //todo. 플레이어 카메라 무빙이 결정될 때 까지만 임시로...

	private void Awake()
	{
		StopPoint = stopPosition.position;

		cam2 = GameObject.Find("Cam_TrashCan").GetComponent<CinemachineVirtualCamera>();
	}

	public void NextAction()
	{
		cam2.Priority = 30;

		PlayerManager.GetInstance().Player.Cleaner.IsUseTrashCan = true;
		OpenTrashCanCover();
	}

	public void ClearAction()
	{
		cam2.Priority = 10;

		PlayerManager.GetInstance().Player.Cleaner.IsUseTrashCan = false;
		trashCanCover.localRotation = Quaternion.identity;
	}

	private void OpenTrashCanCover()
	{
		Coroutines coroutines = new Coroutines();
		StartCoroutine(coroutines.OpenDoorRoutine(trashCanCover, Quaternion.Euler(new Vector3(-50, 0, 0)), 1.5f));
	}
}
