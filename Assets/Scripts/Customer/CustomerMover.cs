using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using static System.Collections.Specialized.BitVector32;

public class CustomerInfo
{
	private Seat _seat;
	public Seat Seat { get { return _seat; } }

	private Chair _chair;
	public Chair Chair { get { return _chair; } }

	//private Chair _anoChair;
	//public Chair AnoChair { get { return _anoChair; } }


	public void Init(Seat seat, int num)
	{
		_seat = seat;
		_chair = _seat.Chairs[num];
		//_anoChair = _seat.Chairs[num == 0 ? 1 : 0];
	}
}

public class CustomerMover : MonoBehaviour
{
	private Coroutine moveRoutine;

	private NavMeshAgent meshAgent;
	private Animator animator;

	public Customer curCustomer;
	public CustomerInfo info;

	public UnityAction<Customer> OnMove;

	private Rigidbody rigidbody;

	private void Awake()
	{
		info = new CustomerInfo();

		meshAgent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
		rigidbody = GetComponent<Rigidbody>();	

		OnMove += Move;
	}

	private void OnDisable()
	{
		if (moveRoutine != null)
		{
			StopCoroutine(moveRoutine);
		}
	}

	private void Move(Customer cust)
	{
		curCustomer = cust;
		moveRoutine = StartCoroutine(MoveRoutine("Move", info.Chair.StopPoint.position));
	}

	IEnumerator MoveRoutine(string action, Vector3 targetPoint)
	{
		animator.SetTrigger(action);

		meshAgent.SetDestination(targetPoint);

		while (true)
		{
			if (Vector3.Distance(targetPoint, transform.position) < 0.1f)
			{
				break;
			}
			yield return null;
		}
		
		meshAgent.ResetPath(); //meshAgent.isStopped = true; //네비게이션 목적지 제거
		TurnToSit();
	}

	private void TurnToSit()
	{
		StopCoroutine(moveRoutine);

		transform.rotation = Quaternion.identity;

		//todo. 손님이 방향을 확 돌아버림. 부드럽게 돌 수 있는 방법을 생각해봐야해 : quaternion lerp

		//Debug.Log(info.Seat.transform.rotation.y);

		float y = info.Chair.transform.rotation.y == 0 ? 90 : -90;
		transform.eulerAngles = new Vector3(0, y, 0);

		//Debug.Log(y);
		
		SitOnAChair(info.Chair.SeatPoints[1].transform);
	}

	private void SitOnAChair(Transform targetPoint)
	{
		//targetPoint.eulerAngles = new Vector3(0, 180, 0);
		//transform.eulerAngles = new Vector3(0, -90, 0);

		animator.SetTrigger("Side");
		moveRoutine = StartCoroutine(MoveToSeatRoutine("Side", targetPoint.position));

		//transform.Translate(targetPoint.position, Space.Self);
	}

	IEnumerator MoveToSeatRoutine(string action, Vector3 targetPoint)
	{
		//Vector3 targetDirection = targetPoint + Vector3.right;
		//float distanceToTarget = Vector3.Distance(transform.position, targetPoint);

		while (Vector3.Distance(targetPoint, transform.position) > 0.4f)
		{
			transform.position = Vector3.MoveTowards(transform.position, targetPoint, Time.deltaTime);

			yield return null;
		}
		animator.SetTrigger("Sit");

		curCustomer.wait.WaitTime = 10;
		curCustomer.wait.onWait?.Invoke(curCustomer);
	}
}
