using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

//��ư�� �ڽſ� �°� �ڵ������� �� �ִ� ����� ������?

public class CookListUI : InGameUI
{
	[SerializeField]
	private Transform BtnContent;

	CookListController uiController;

	protected override void Awake()
	{
		base.Awake();

		uiController = new CookListController(new CookListModel());
		InitButton();
	}

	private void OnEnable()
	{
		uiController.SetBtnContent();
	}

	private void InitButton()
	{
		uiController.SetCookBtn(BtnContent.GetComponentsInChildren<Button>());
	}

	/// <summary>
	/// CookListUI�� ȣ���� ���� ������Ʈ ����
	/// </summary>
	/// <param name="oven"></param>
	public void SetCurrentOven(HearthOven oven)
	{
		uiController.SetCalledOver(oven);
	}
}

public class CookListController
{
	private CookListModel model;

	public CookListController(CookListModel model)
	{
		this.model = model;
	}

	public void SetCookBtn(Button[] buttons)
	{
		model.CookList = buttons;

		foreach (var btn in model.CookList)
		{
			btn.transform.AddComponent<Cookable>();
		}
	}

	public void SetCalledOver(HearthOven oven)
	{
		model.CalledOven = oven;

		foreach (var btn in model.CookList)
		{
			var cookable = btn.transform.GetComponent<Cookable>();
			cookable.Oven = model.CalledOven;
		}
	}

	public void SetBtnContent()
	{
		var orderList = FoodManager.GetInstance().GetOrderList();

		int index = 0;
		foreach (var food in orderList)
		{
			var btn = model.CookList[index++];
			btn.image.sprite = food.Value.Icon;

			var cookable = btn.transform.GetComponent<Cookable>();
			cookable.FoodData = food.Value;
		}
	}
}

public class CookListModel
{
	public Button[] CookList;

	public HearthOven CalledOven;
}