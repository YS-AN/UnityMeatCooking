using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour, IMoveable
{
	private const string UI_PATH = "UI/OpenStorage";

	private OpenStorage openStorage;

	[SerializeField]
	private Transform storageDoor;

	[SerializeField]
	private Transform StopPosition;
	public Vector3 StopPoint { get; set; }

	private void Awake()
	{
		StopPoint = StopPosition.position;
	}

	public void NextAction()
	{
		openStorage = GameManager.UI.ShowInGameUI<OpenStorage>(UI_PATH);
		openStorage.SetTarget(transform);
		openStorage.OnOpenDoor += OpenStorageDoor;
	}

	public void ClearAction()
	{
		if(openStorage != null)
		{
			GameManager.UI.CloseInGameUI(openStorage);
			storageDoor.eulerAngles = new Vector3(0, -90, 0);
			openStorage = null;
		}
	}

	public void OpenStorageDoor()
	{
		Coroutines coroutines = new Coroutines();
		StartCoroutine(coroutines.OpenDoorRoutine(storageDoor, Quaternion.Euler(new Vector3(0, 0, 0))));
	}
}
