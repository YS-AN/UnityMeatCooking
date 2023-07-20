using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Shadow : MonoBehaviour
{
	public bool IsEnterOffLimits;

	public bool IsOutsideStore;

	public UnityAction OnPlacedOffLimits;

	private void Awake()
	{
		IsEnterOffLimits = false;
		OnPlacedOffLimits += PlacedOffLimits;
	}

	private void PlacedOffLimits()
	{
		if(IsEnterOffLimits)
		{
			IsEnterOffLimits = false;
			GuidMessageManager.GetInstance().ShowMessage("�ش� ��ġ���� ������ ��ġ�� �� �����ϴ�.");
		}
	}
}
