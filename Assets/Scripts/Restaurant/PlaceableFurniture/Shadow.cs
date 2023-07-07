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
			GuidMessageManager.GetInstance().ShowMessage("이동 경로에는 가구를 배치할 수 없습니다.");
		}
		
	}
}
