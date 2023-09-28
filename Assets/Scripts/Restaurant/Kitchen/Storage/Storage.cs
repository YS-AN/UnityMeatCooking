using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
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

	public void OpenStorageUI()
	{
		openStorage = GameManager.UI.ShowInGameUI<OpenStorage>(UI_PATH_BTN);
		openStorage.SetTarget(transform);
		openStorage.OnOpenDoor += OpenStorageDoor;
	}

	public void ClearStorageUI()
	{
		if(OpenDoorRoutine != null)
		{
			StopCoroutine(OpenDoorRoutine);
			OpenDoorRoutine = null;
		}

		if(openStorage != null)
		{
			openStorage.OnOpenDoor -= OpenStorageDoor;
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
		OpenDoorRoutine = StartCoroutine(coroutines.LocalBasedRotationRoutine(storageDoor, Quaternion.Euler(new Vector3(0, 90, 0)), 0.2f, OpenInventroy));
	}

	private void OpenInventroy()
	{
		if(OpenDoorRoutine != null)
		{
			if(inventoryUI == null)
			{
				inventoryUI = GameManager.UI.ShowInGameUI<InventoryUI>(UI_PATH_INV);
				invPickUI = GameManager.UI.ShowInGameUI<InvPickupUI>(UI_PATH_PIK);
			}
		}
	}
}
