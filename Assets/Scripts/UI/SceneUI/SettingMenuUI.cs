using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingMenuUI : SceneUI
{
	[SerializeField]
	private Decoration decoration;

	private RestaurantUI restaurantUI;

	protected override void Awake()
	{
		base.Awake();

		buttons["BtnSetting"].onClick.AddListener(() => { ClickBtnSetting(); } );
		buttons["BtnDecoration"].onClick.AddListener(() => { ClickBtnDecoration(); });
		buttons["BtnPause"].onClick.AddListener(() => { ClickBtnPause(); });

		restaurantUI = transform.GetComponentInParent<RestaurantUI>();
	}

	private void ClickBtnSetting()
	{

	}

	private void ClickBtnDecoration()
	{
		restaurantUI.gameObject.SetActive(false);

		decoration.OnEndDecoration += EndDecoration;
		decoration.OnStartDecoration?.Invoke();
	}

	private void EndDecoration()
	{
		restaurantUI.gameObject.SetActive(true);
	}

	private void ClickBtnPause()
	{

	}
}
