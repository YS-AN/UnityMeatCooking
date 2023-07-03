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

		StorageManager.GetInstance().OnSelectIngr += uiController.AddPickIngr;
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

	public void AddPickIngr(int btnId)
	{
		var existedBtn = model.Inventory.Where(x => x.GetComponent<PikBtnIngr>().BtnId == btnId).FirstOrDefault();
		if(existedBtn != null)
		{
			existedBtn.GetComponent<PikBtnIngr>().AddCount(1);
		}
		else
		{
			var newBtn = model.Inventory.Where(x => x.GetComponent<PikBtnIngr>().BtnId == -1).FirstOrDefault();
			if (newBtn != null)
				newBtn.GetComponent<PikBtnIngr>().SetBtnInfo(btnId);
		}
	}
}

public class InvPickupModel
{
	public Button[] Inventory;
}