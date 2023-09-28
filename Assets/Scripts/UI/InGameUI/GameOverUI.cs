using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : InGameUI
{
	public TMP_Text txtGameOverMessage;

	protected override void Awake()
	{
		base.Awake();
	}

	private void Start()
	{
		InitGameData();
	}

	public void SetGameOverMessage(string overMessage)
	{
		txtGameOverMessage.text = overMessage;
	}

	private void InitGameData()
	{
		FoodManager.GetInstance().RemoveAllOrder();
	}
}
