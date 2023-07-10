using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bankrupt : MonoBehaviour
{
	private const string UI_PATH = "UI/GameOverUI";

	GameOverUI gameOverUI = null;
	public UnityAction OnBankrupt;

	public void Awake()
	{
		OnBankrupt += OpenGameOverUI;
	}

	private void OpenGameOverUI()
	{
		PlayerManager.GetInstance().Player.Animator.SetTrigger("IsDown");

		gameOverUI = GameManager.UI.ShowInGameUI<GameOverUI>(UI_PATH);
	}

}