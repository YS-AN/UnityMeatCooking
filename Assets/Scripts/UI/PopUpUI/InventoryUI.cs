using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class InventoryUI : PopUpUI
{
	[SerializeField]
	private Transform BtnContent;

	private InventoryController uiController;

	protected override void Awake()
	{
		base.Awake();

		uiController = new InventoryController(new InventoryModel());
		InitButton();
	}

	private void OnEnable()
	{
		uiController.SetBtnContent();
	}

	private void OnDisable()
	{
		uiController.ClearInventoryImage();
	}

	private void InitButton()
	{
		uiController.SetCookBtn(BtnContent.GetComponentsInChildren<Button>());
	}
}

public class InventoryController
{
	private InventoryModel model;

	public InventoryController(InventoryModel model)
	{
		this.model = model;
	}

	public void SetCookBtn(Button[] buttons)
	{
		model.Inventory = buttons;

		foreach (var btn in model.Inventory)
		{
			
		}
	}

	public void SetCalledOver(HearthOven oven)
	{
	}

	public void SetBtnContent()
	{
		var orderList = FoodManager.GetInstance().GetOrderList();

		int index = 0;
		foreach (var food in orderList)
		{
			var btn = model.Inventory[index++];
			btn.image.sprite = food.Value.FoodInfo.Icon;

			var cookable = btn.transform.GetComponent<AddPocket>();
		}
	}

	public void ClearInventoryImage()
	{
		for (int i = 0; i < model.Inventory.Length; i++)
		{
			model.Inventory[i].image.sprite = null;
		}
	}
}

public class InventoryModel
{
	public Button[] Inventory;
}