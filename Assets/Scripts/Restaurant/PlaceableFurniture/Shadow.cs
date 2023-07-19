using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Shadow : MonoBehaviour
{
	public bool IsEnterOffLimits;

	[SerializeField]
	private LayerMask groundMask;

	public UnityAction OnPlacedOffLimits;

	private void Awake()
	{
		IsEnterOffLimits = false;
		OnPlacedOffLimits += PlacedOffLimits;
	}

	private void Update()
	{
		IsGround();
	}

	private void PlacedOffLimits()
	{
		if(IsEnterOffLimits)
		{
			IsEnterOffLimits = false;
			GuidMessageManager.GetInstance().ShowMessage("이동 경로에는 가구를 배치할 수 없습니다.");
		}
	}

	private void IsGround()
	{
		float groundDistance = 10f;

		//*
		// groundCheck 위치에서 아래로 BoxCast를 수행하여 충돌을 검사합니다.
		//bool isGrounded = Physics.BoxCast(transform.position, transform.localScale / 2, Vector3.down, Quaternion.identity, groundDistance, groundMask);
		bool isGrounded = Physics.Raycast(transform.position, Vector3.down, out var hit, groundDistance, groundMask);

	
		if (isGrounded)
		{
			// 땅에 닿은 경우 실행할 코드를 여기에 작성합니다.
			Debug.Log("땅에 있습니다!");
		}
		else
		{
			// 땅에 닿지 않은 경우 실행할 코드를 여기에 작성합니다.
			Debug.Log("땅에 없습니다!");
		}
	}

	private void OnDrawGizmos()
	{
		
	}


	private void OnCollisionStay(Collision collision)
	{

		//Debug.Log("OnCollisionStay");
	}

	private void OnCollisionEnter(Collision collision)
	{
		//Debug.Log("OnCollisionEnter");
	}

	private void OnCollisionExit(Collision collision)
	{
		//Debug.Log("OnCollisionExit");
	}

	private void OnTriggerEnter(Collider other)
	{
		//Debug.Log("OnTriggerEnter");
	}

	private void OnTriggerStay(Collider other)
	{
		if(groundMask.IsContain(gameObject.layer))
		{
			//Debug.Log("땅\!");

		}

		//Debug.Log("OnTriggerStay");
	}

	private void OnTriggerExit(Collider other)
	{
		if (groundMask.IsContain(gameObject.layer))
		{
			Debug.Log("땅에 없습니다!");
		}

	}
}
