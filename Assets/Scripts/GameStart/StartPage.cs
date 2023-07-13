using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartPage : MonoBehaviour
{
	[SerializeField]
	private Button BtnStart;

	[SerializeField]
	private Button BtnExit;

	public void SetBtnOnClicked()
	{
		BtnStart.onClick.AddListener(() => { GameStart(); });
		BtnExit.onClick.AddListener(() => { ExitGame(); });
	}

	private void GameStart()
	{
		SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
	}

	private void ExitGame()
	{
		Application.Quit();
	}
}