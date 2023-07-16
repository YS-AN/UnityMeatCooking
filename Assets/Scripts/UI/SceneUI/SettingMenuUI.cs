using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingMenuUI : SceneUI
{
	[SerializeField]
	private Decoration decoration;

	private Bankrupt bankrupt;

	private RestaurantUI restaurantUI;

	protected override void Awake()
	{
		base.Awake();

		buttons["BtnSetting"].onClick.AddListener(() => { ClickBtnSetting(); } );
		buttons["BtnDecoration"].onClick.AddListener(() => { ClickBtnDecoration(); });
		buttons["BtnPause"].onClick.AddListener(() => { ClickBtnPause(); });

		restaurantUI = transform.GetComponentInParent<RestaurantUI>();
		bankrupt = transform.GetComponentInParent<Bankrupt>();
	}

	private void ClickBtnSetting()
	{ 
		if (GameManager.Data.IsGamePause == false)
		{
			GameManager.UI.ShowPopUpUI<SettingUI>("UI/GameSetting");
		}
	}

	private void ClickBtnDecoration()
	{
		if (GameManager.Data.IsGamePause == false)
		{
			restaurantUI.gameObject.SetActive(false);

			decoration.OnEndDecoration += EndDecoration;
			decoration.OnStartDecoration?.Invoke();
		}
	}

	private void EndDecoration()
	{
		restaurantUI.gameObject.SetActive(true);
	}

	private void ClickBtnPause()
	{
		string btnMsg;
		if(GameManager.Data.IsGamePause == false)
		{
			GameManager.Data.IsGamePause = true;
			btnMsg = "다시시작"; 
		}
		else
		{
			GameManager.Data.IsGamePause = false;
			btnMsg = "일시정지";
		}
		Time.timeScale = GameManager.Data.IsGamePause ? 0 : 1;
		buttons["BtnPause"].transform.GetComponentInChildren<TextMeshProUGUI>().text = btnMsg;
	}
}
