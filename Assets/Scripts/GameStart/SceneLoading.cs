using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoading : MonoBehaviour
{
	[SerializeField]
	private Slider progressbar;

	[SerializeField]
	private Canvas loadingCanvas;

	[SerializeField]
	private GameTimeCounter Gametimer;

	private void Start()
	{
		StartCoroutine(LoadScene());
	}

	private void OnEnable()
	{
	}

	IEnumerator LoadScene()
	{
		InitGameSetting();

		while (progressbar.value < 1f)
		{
			yield return null;
			progressbar.value = Mathf.MoveTowards(progressbar.value, 1f, Time.deltaTime);
		}
		FinishedLoad();
	}

	private void InitGameSetting()
	{
		GameManager.Pool.InitPool();
		GameManager.UI.InitUICanvas();
		
		FoodManager.GetInstance().RemoveAllOrder();
	}

	private void FinishedLoad()
	{
		GameManager.Data.IsOpenrestaurant = true;

		Destroy(loadingCanvas.gameObject);

		AudioManager.GetInstance().PlayBgm(true);

		Gametimer.OnStartGameTimeCount?.Invoke();
	}
}
