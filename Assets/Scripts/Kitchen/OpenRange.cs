using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OpenRange : MonoBehaviour
{
	private const string UI_PATH = "UI/OpenStorage";

	[SerializeField]
	private LayerMask PlayerMask;

	[SerializeField]
	private Transform StorageDoor;

	private OpenStorage openStorage;

	private void OnTriggerEnter(Collider other)
	{
		//Debug.Log("IsOpen?");

		if (PlayerMask.IsContain(other.gameObject.layer))
		{
			Debug.Log(StorageDoor.rotation);

			openStorage = GameManager.UI.ShowInGameUI<OpenStorage>(UI_PATH);
			openStorage.SetTarget(transform);
			openStorage.StorageDoor = StorageDoor;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		//Debug.Log("BYE?");

		if (PlayerMask.IsContain(other.gameObject.layer))
		{
			if(openStorage  != null)
			{
				GameManager.UI.CloseInGameUI(openStorage);
				StorageDoor.eulerAngles = new Vector3(0, -90, 0);
			}
		}
	}
}
