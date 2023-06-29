using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomerRater : MonoBehaviour
{
	private const string UI_PATH = "UI/CustRateUI";
	private CustRateUI custRateUI;

	public UnityAction<CustomerRateType> OnRate;

	private void Awake()
	{
		OnRate += CustomerRate;
		custRateUI = null;
	}

	private void CustomerRate(CustomerRateType rateType)
	{
		custRateUI = GameManager.UI.ShowInGameUI<CustRateUI>(UI_PATH);
		custRateUI.SetTarget(transform);
		custRateUI.SetCustomerRate(rateType);
	}

	public void CloseRateView()
	{
		if(custRateUI != null)
		{
			GameManager.UI.CloseInGameUI(custRateUI);
			custRateUI = null;
		}
	}
}
