using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenStorage : InGameUI
{
	public Transform StorageDoor;

	protected override void Awake()
	{
		base.Awake();

		buttons["BtnStgOpen"].onClick.AddListener(() => { OpenInventory(); });
	}

	public void OpenInventory()
	{
		StartCoroutine(OpenDoorRoutine());
		//storage.OnOpenDoor?.Invoke();
	}

	private IEnumerator OpenDoorRoutine()
	{
		Quaternion startRotation = StorageDoor.rotation;
		Quaternion targetRotation = Quaternion.Euler(0, 0, 0);

		float elapsedTime = 0f;
		float duration = 3f; // 전환에 걸리는 시간

		while (elapsedTime < duration)
		{
			float time = elapsedTime / duration;
 			StorageDoor.rotation = Quaternion.Lerp(startRotation, targetRotation, time);

 			elapsedTime += Time.deltaTime;
			yield return null;
		}
		StorageDoor.rotation = targetRotation;
	}
}
