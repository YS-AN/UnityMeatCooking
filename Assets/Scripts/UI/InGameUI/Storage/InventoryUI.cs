using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventoryUI : InGameUI
{
	private const string NM_BTN_ALL = "BtnAll";
	private const string NM_BTN_MEAT = "BtnMeat";
	private const string NM_BTN_GNSH = "BtnGarnish";

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
		uiController.SetBtnContent(IngredientType.None);
		StorageManager.GetInstance().OnDeselectIngr += uiController.AddDeselectIngr;
	}

	private void OnDisable()
	{
		uiController.ClearInventoryImage();
		StorageManager.GetInstance().OnDeselectIngr -= uiController.AddDeselectIngr;
	}

	private void InitButton()
	{
		uiController.SetCookBtn(BtnContent.GetComponentsInChildren<Button>());

		buttons[NM_BTN_ALL].onClick.AddListener(() => { uiController.SetBtnContent(IngredientType.None); });
		buttons[NM_BTN_MEAT].onClick.AddListener(() => { uiController.SetBtnContent(IngredientType.Meat); });
		buttons[NM_BTN_GNSH].onClick.AddListener(() => { uiController.SetBtnContent(IngredientType.Garnish); });
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

		foreach(var btn in model.Inventory)
		{
			btn.GetComponent<InvBtnIngr>().InitData();
		}
	}

	public void SetBtnContent(IngredientType type)
	{
		ClearInventoryImage();

		var havingList = StorageManager.GetInstance().GetInventoryList(type);

		int index = 0;
		foreach (var item in havingList)
		{
			var btn = model.Inventory[index++];
			var btnIngr = btn.GetComponent<InvBtnIngr>();

			btnIngr.SetBtnInfo(item.Key, item.Value, item.Value.Count);
		}
	}

	public void ClearInventoryImage()
	{
		for (int i = 0; i < model.Inventory.Length; i++)
		{
			var btnIngr = model.Inventory[i].GetComponent<InvBtnIngr>();

			btnIngr.ClearBtnInfo();
		}
	}

	public void AddDeselectIngr(IngredientName btnId)
	{
		var existedBtn = model.Inventory.Where(x => x.GetComponent<InvBtnIngr>().BtnId == btnId).FirstOrDefault();
		if (existedBtn != null)
		{
			existedBtn.GetComponent<InvBtnIngr>().Info.Count += 1;
		}
		else
		{
			var newBtn = model.Inventory.Where(x => x.GetComponent<InvBtnIngr>().BtnId == IngredientName.None).FirstOrDefault();
			if (newBtn != null)
			{
				IngrInfo info = StorageManager.GetInstance().Ingredients[btnId];
				newBtn.GetComponent<InvBtnIngr>().SetBtnInfo(btnId, info, 1);
			}
		}
	}
}

public class InventoryModel
{
	public Button[] Inventory;
}