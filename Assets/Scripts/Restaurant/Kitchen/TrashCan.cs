using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
	[SerializeField]
	private Transform trashCanCover;


	public void ThrowAwayTrash()
	{
		Debug.Log("ThrowAwayTrash");

		PlayerManager.GetInstance().Player.Cleaner.IsUseTrashCan = true;
		OpenTrashCanCover();
	}

	public void AfterThrewAway()
	{
		Debug.Log("AfterThrewAway");

		PlayerManager.GetInstance().Player.Cleaner.IsUseTrashCan = false;
		trashCanCover.localRotation = Quaternion.identity;
	}

	private void OpenTrashCanCover()
	{
		Coroutines coroutines = new Coroutines();
		StartCoroutine(coroutines.LocalBasedRotationRoutine(trashCanCover, Quaternion.Euler(new Vector3(-50, 0, 0)), 1.5f));
	}
}
