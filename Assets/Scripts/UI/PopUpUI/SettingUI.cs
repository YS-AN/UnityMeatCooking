using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : PopUpUI
{
	[SerializeField]
	private Button BtnClose;

	[SerializeField]
	private Slider bgmVolume;

	protected override void Awake()
	{
		BtnClose.onClick.AddListener(() => CloseView());

		bgmVolume.onValueChanged.AddListener((volume) => { ChangeBgmVolume(volume); });
	}

	private void CloseView()
	{
		GameManager.UI.ClosePopUpUI();
	}

	private void ChangeBgmVolume(float volume)
	{
		AudioManager.GetInstance().SetBgmVolume(volume);
	}
}
