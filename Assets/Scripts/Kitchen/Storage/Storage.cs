using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour, IMoveable
{
	private const string UI_PATH_BTN = "UI/OpenStorage";
	private const string UI_PATH_INV = "UI/InventroyUI";
	private const string UI_PATH_PIK = "UI/InvPickupUI";

	private OpenStorage openStorage;
	private InventoryUI inventoryUI;
	private InvPickupUI invPickUI;

	private Coroutine OpenDoorRoutine;

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
		openStorage = GameManager.UI.ShowInGameUI<OpenStorage>(UI_PATH_BTN);
		openStorage.SetTarget(transform);
		openStorage.OnOpenDoor += OpenStorageDoor;
	}

	public void ClearAction()
	{
		if(OpenDoorRoutine != null)
		{
			StopCoroutine(OpenDoorRoutine);
			OpenDoorRoutine = null;
		}

		if(openStorage != null)
		{
			GameManager.UI.CloseInGameUI(openStorage);
			openStorage = null;

			storageDoor.localRotation = Quaternion.identity;
		}

		if (inventoryUI != null)
		{
			GameManager.UI.CloseInGameUI(inventoryUI);
			inventoryUI = null;
		}

		if (invPickUI != null)
		{
			GameManager.UI.CloseInGameUI(invPickUI);
			invPickUI = null;
		}
	}

	public void OpenStorageDoor()
	{
		Coroutines coroutines = new Coroutines();
		OpenDoorRoutine = StartCoroutine(coroutines.OpenDoorRoutine(storageDoor, Quaternion.Euler(new Vector3(0, 90, 0)), 2, OpenInventroy));
	}

	private void OpenInventroy()
	{
		if(OpenDoorRoutine != null)
		{
			inventoryUI = GameManager.UI.ShowInGameUI<InventoryUI>(UI_PATH_INV);
			invPickUI = GameManager.UI.ShowInGameUI<InvPickupUI>(UI_PATH_PIK);
		}
	}
}
