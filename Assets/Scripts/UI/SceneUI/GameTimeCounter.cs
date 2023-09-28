using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameTimeCounter : MonoBehaviour
{
	private const string UI_PATH = "UI/GameOverUI";

	[SerializeField]
	private TMP_Text txtGameTime;

	[SerializeField]
	private float totalTime = 300f; //5분

	private float gameTime = 0.1f;

	public UnityAction OnStartGameTimeCount;

	private void Awake()
	{
		OnStartGameTimeCount += SetStartTimeCount;
	}

	private void SetStartTimeCount()
	{
		StartCoroutine(TimeCounterRoutine());
	}

	private IEnumerator TimeCounterRoutine()
	{
		gameTime = totalTime;

		while (gameTime > 0f)
		{
			// 시간 형식으로 변환하여 텍스트에 표시
			int minutes = Mathf.FloorToInt(gameTime / 60f);
			int seconds = Mathf.FloorToInt(gameTime % 60f);
			txtGameTime.text = string.Format("{0:D2}:{1:D2}", minutes, seconds);

			yield return new WaitForSeconds(1);
			gameTime--;
		}
		GameOverUI gameOverUI = GameManager.UI.ShowInGameUI<GameOverUI>(UI_PATH);
		gameOverUI.SetGameOverMessage("영업시간이 끝났습니다. 영업을 종료합니다.");
	}
}
