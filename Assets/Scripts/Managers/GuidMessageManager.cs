using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidMessageManager : MonoBehaviour
{
	private static GuidMessageManager instance;

	[SerializeField]
	private MessageUI messageUI;

	public static GuidMessageManager GetInstance()
	{
		if (instance == null)
			instance = new GuidMessageManager();

		return instance;
	}

	private void Awake()
	{
		instance = this; 
	}

	public void ShowMessage(string message, int displayTime = 3)
	{
		messageUI.ShowMessage(message, displayTime);
	}
}
