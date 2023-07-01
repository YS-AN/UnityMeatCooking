using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OpenStorage : InGameUI
{
	private const string UI_PATH = "UI/InventroyUI";

	public Transform StorageDoor;

	public UnityAction OnOpenDoor;

	protected override void Awake()
	{
		base.Awake();

		buttons["BtnStgOpen"].onClick.AddListener(() => { OpenInventory(); });
	}

	private void OpenInventory()
	{
		OnOpenDoor?.Invoke();
	}

}
