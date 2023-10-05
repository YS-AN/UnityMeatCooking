using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class CustRateUI : InGameUI
{
	private string[] imageList;
	private Image pickImg; 

	protected override void Awake()
	{
		base.Awake();

		SetImageList();
	}

	private void OnDisable()
	{
		pickImg.gameObject.SetActive(false);
	}

	private void SetImageList()
	{
		imageList = new string[] { "ImgGood", "ImgAngry", "ImgUnder", "ImgOver", "ImgNone" };

		foreach(var item in imageList)
		{
			images[item].gameObject.SetActive(false);
		}
	}

	public void SetCustomerRate(CustomerRateType rateType)
	{
		pickImg = images[imageList[(int)rateType]];
		pickImg.gameObject.SetActive(true);
	}
}


