using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public enum CustomerRateType
{
	Good,
	Angry,
	Under,
	Over,
	None
}

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
		imageList = new string[5];

		imageList[0] = "ImgGood";
		imageList[1] = "ImgAngry";
		imageList[2] = "ImgUnder";
		imageList[3] = "ImgOver";
		imageList[4] = "ImgNone";

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


