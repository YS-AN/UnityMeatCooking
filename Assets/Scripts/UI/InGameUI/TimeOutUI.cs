using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TimeOutUI : InGameUI
{
	[SerializeField]
	private Button btnGameStart;

	public UnityAction OnContinueGame;

	protected override void Awake()
	{
		base.Awake();

		btnGameStart.onClick.AddListener(SetContinedGame);
	}

	private void SetContinedGame()
	{
		OnContinueGame?.Invoke();
		OnContinueGame = null; //모든 참조 해지

		GameManager.UI.CloseInGameUI(this);
	}
}
