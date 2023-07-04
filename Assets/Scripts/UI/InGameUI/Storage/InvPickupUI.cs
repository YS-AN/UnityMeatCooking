using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InvPickupUI : InGameUI
{
	[SerializeField]
	private Transform BtnContent;

	private InvPickupController uiController;


	protected override void Awake()
	{
		base.Awake();

		uiController = new InvPickupController(new InvPickupModel());
		uiController.InitPickBtn(BtnContent.GetComponentsInChildren<Button>());
	}

	private void OnEnable()
	{
		uiController.SetBtnContent(IngredientType.None);
		StorageManager.GetInstance().OnSelectIngr += uiController.AddPickIngr;
	}

	private void OnDisable()
	{
		uiController.ClearInventoryImage();
		StorageManager.GetInstance().OnSelectIngr -= uiController.AddPickIngr;
	}
}


public class InvPickupController
{
	private InvPickupModel model;

	public InvPickupController(InvPickupModel model)
	{
		this.model = model;
	}

	public void InitPickBtn(Button[] buttons)
	{
		model.Inventory = buttons;

		foreach (var btn in model.Inventory)
		{
			btn.GetComponent<PikBtnIngr>().InitData();
		}
	}

	public void AddPickIngr(IngredientName btnId)
	{
		var existedBtn = model.Inventory.Where(x => x.GetComponent<PikBtnIngr>().BtnId == btnId).FirstOrDefault();
		if(existedBtn != null)
		{
			existedBtn.GetComponent<PikBtnIngr>().AddPickIngrCount(1);
		}
		else
		{
			var newBtn = model.Inventory.Where(x => x.GetComponent<PikBtnIngr>().BtnId == IngredientName.None).FirstOrDefault();
			if (newBtn != null)
				newBtn.GetComponent<PikBtnIngr>().SetBtnInfo(btnId, StorageManager.GetInstance().Ingredients[btnId], 1);
		}
	}

	public void SetBtnContent(IngredientType type)
	{
		ClearInventoryImage();

		int index = 0;
		foreach (var item in StorageManager.GetInstance().HavingList)
		{
			var btn = model.Inventory[index++];
			var btnIngr = btn.GetComponent<PikBtnIngr>();

			btnIngr.SetBtnInfo(item.Key, item.Value);
		}
	}

	public void ClearInventoryImage()
	{
		for (int i = 0; i < model.Inventory.Length; i++)
		{
			var btnIngr = model.Inventory[i].GetComponent<PikBtnIngr>();

			if (btnIngr.BtnId >= 0)
			{
				btnIngr.ClearBtnInfo(false);
			}
		}
	}
}

public class InvPickupModel
{
	public Button[] Inventory;
}