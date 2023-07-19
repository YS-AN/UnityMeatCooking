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
			GuidMessageManager.GetInstance().ShowMessage("�̵� ��ο��� ������ ��ġ�� �� �����ϴ�.");
		}
	}

	private void IsGround()
	{
		float groundDistance = 10f;

		//*
		// groundCheck ��ġ���� �Ʒ��� BoxCast�� �����Ͽ� �浹�� �˻��մϴ�.
		//bool isGrounded = Physics.BoxCast(transform.position, transform.localScale / 2, Vector3.down, Quaternion.identity, groundDistance, groundMask);
		bool isGrounded = Physics.Raycast(transform.position, Vector3.down, out var hit, groundDistance, groundMask);

	
		if (isGrounded)
		{
			// ���� ���� ��� ������ �ڵ带 ���⿡ �ۼ��մϴ�.
			Debug.Log("���� �ֽ��ϴ�!");
		}
		else
		{
			// ���� ���� ���� ��� ������ �ڵ带 ���⿡ �ۼ��մϴ�.
			Debug.Log("���� �����ϴ�!");
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
			//Debug.Log("��\!");

		}

		//Debug.Log("OnTriggerStay");
	}

	private void OnTriggerExit(Collider other)
	{
		if (groundMask.IsContain(gameObject.layer))
		{
			Debug.Log("���� �����ϴ�!");
		}

	}
}
