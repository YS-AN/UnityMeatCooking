using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingMenuUI : SceneUI
{
	private void Awake()
	{
		base.Awake();

		buttons["BtnSetting"].onClick.AddListener(() => { ClickBtnSetting(); } );
		buttons["BtnDecoration"].onClick.AddListener(() => { ClickBtnDecoration(); });
		buttons["BtnPause"].onClick.AddListener(() => { ClickBtnPause(); });
	}

	private void ClickBtnSetting()
	{

	}

	private void ClickBtnDecoration()
	{

	}

	private void ClickBtnPause()
	{

	}
}
