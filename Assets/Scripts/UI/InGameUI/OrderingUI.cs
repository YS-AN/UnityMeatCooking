using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class OrderingUI : InGameUI
{
	private const string NM_FOOD_IMG = "ImgFood";
	private const string NM_WAITBAR = "WaitBar";
	private const string NM_WAIT_LAYER = "Waiting";

	protected override void Awake()
	{
		base.Awake();
	}

	public void SetFoodIcon(Sprite foodIcon)
	{ 
		images[NM_FOOD_IMG].sprite = foodIcon;
	}

	public void StartWait(Customer curCust, int waitMax, int waitTime = 1)
	{
		WaitBar waitBar = images[NM_WAIT_LAYER].GetComponent<WaitBar>();
		//waitBar.SetTarget(transform);
		waitBar.customer = curCust;

		waitBar.StartSlider(waitMax, waitTime);
	}

	public void StopWait()
	{
		//waitBar.StopSlider();
	}

}
