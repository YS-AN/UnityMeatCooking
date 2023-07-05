using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class MessageUI : SceneUI
{
	private const string TXT_GUIDE_MSG = "TxtGuideMessage";

	private Coroutine showRoutine;

	protected override void Awake()
	{
		base.Awake();
	}

	public void ShowMessage(string message, int displayTime)
	{
		if(showRoutine != null)
			StopCoroutine(showRoutine);

		texts[TXT_GUIDE_MSG].text = message;

		Coroutines coroutines = new Coroutines();
		showRoutine = StartCoroutine(coroutines.JustWaitRoutine(displayTime, DisappearMessage));
	}

	private void DisappearMessage()
	{
		texts[TXT_GUIDE_MSG].text = "";

		StopCoroutine(showRoutine);
		showRoutine = null;
	}
}

