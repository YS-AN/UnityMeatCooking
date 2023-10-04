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
		PlayerManager.GetInstance().Player.Cleaner.IsUseTrashCan = true;
	}

	public void AfterThrewAway()
	{
		PlayerManager.GetInstance().Player.Cleaner.IsUseTrashCan = false;
	}

	private void OpenTrashCanCover()
	{
		Coroutines coroutines = new Coroutines();
		StartCoroutine(coroutines.LocalBasedRotationRoutine(trashCanCover, Quaternion.Euler(new Vector3(-50, 0, 0)), 1.5f));
	}
}
