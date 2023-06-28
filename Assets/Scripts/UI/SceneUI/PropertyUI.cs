using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyUI : SceneUI
{
	protected override void Awake()
	{
		base.Awake();

		GameManager.Data.OnChangeRevenue += UpdateRevenue;
	}

	private void UpdateRevenue()
	{
		texts["txtRevenue"].text = GameManager.Data.RevenueToStr;
	}
}
