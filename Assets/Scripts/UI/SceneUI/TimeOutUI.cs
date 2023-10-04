using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TimeOutUI : SceneUI
{
	[SerializeField]
	private Decoration decoration;

	public UnityAction OnContinueGame;

	protected override void Awake()
	{
		base.Awake();

		buttons["BtnRestart"].onClick.AddListener(SetContinedGame);
		buttons["BtnEnd"].onClick.AddListener(EndGame);

		decoration.OnStartDecoration += StartDecoration;
		decoration.OnEndDecoration += EndDecoration;
	}

	private void SetContinedGame()
	{
		OnContinueGame?.Invoke();
		OnContinueGame = null;

		GameManager.Data.IsOpenRestaurant = true;
		GameManager.Data.IsOrder = true;
		PlayerManager.GetInstance().IsMove = true;

		gameObject.SetActive(false);
	}

	private void EndGame()
	{
		Application.Quit();
	}

	private void StartDecoration()
	{
		gameObject.SetActive(false);
	}

	private void EndDecoration()
	{
		gameObject.SetActive(true);
	}
}