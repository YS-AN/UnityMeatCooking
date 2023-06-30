using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChefCleaner : MonoBehaviour
{
	public bool IsUseTrashCan;

	private void Awake()
	{
		IsUseTrashCan = false;
	}
}
