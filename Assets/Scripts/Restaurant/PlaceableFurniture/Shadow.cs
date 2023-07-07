using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Shadow : MonoBehaviour
{
	public bool IsEnterOffLimits;

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
			GuidMessageManager.GetInstance().ShowMessage("�̵� ��ο��� ������ ��ġ�� �� �����ϴ�.");
		}
		
	}
}
