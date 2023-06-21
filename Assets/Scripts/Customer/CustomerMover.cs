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

public class CustomerMover : MonoBehaviour
{
	private Coroutine moveRoutine;

	private NavMeshAgent meshAgent;
	private Animator animator;

	public Customer curCustomer;
	public CustSeatInfo info;

	public UnityAction<Customer> OnEnter;
	public UnityAction<Customer> OnExit;

	private Rigidbody rigidbody;

	private void Awake()
	{
		info = new CustSeatInfo();

		meshAgent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
		rigidbody = GetComponent<Rigidbody>();

		OnEnter += Enter;
		OnExit += Exit;
	}

	private void OnDisable()
	{
		if (moveRoutine != null)
		{
			StopCoroutine(moveRoutine);
		}
	}

	/// <summary>
	/// 의자로 이동
	/// </summary>
	/// <param name="cust"></param>
	private void Enter(Customer cust)
	{
		curCustomer = cust;
		animator.SetTrigger("Move");

		Coroutines coroutines = new Coroutines();
		moveRoutine = StartCoroutine(coroutines.MoveRoutine(meshAgent, transform, info.Chair.StopPoint.position, TurnToSit));
	}

	/// <summary>
	/// 의자와 같은 방향을 바로보도록 회전
	/// </summary>
	private void TurnToSit()
	{
		StopCoroutine(moveRoutine);

		transform.rotation = Quaternion.identity;

		float y = info.Chair.transform.rotation.y == 0 ? 90 : -90;
		transform.rotation = Quaternion.Euler(0, y, 0);

		int seatNum = (int)Random.Range(0f, info.Chair.SeatPoints.Count());
		SitOnAChair(info.Chair.SeatPoints[seatNum].transform);
	}

	/// <summary>
	/// 지정된 위치에 앉도록 이동
	/// </summary>
	/// <param name="targetPoint">앉을 위치</param>
	private void SitOnAChair(Transform targetPoint)
	{
		animator.SetTrigger("Side");

		Vector3 destination = new Vector3(transform.position.x, transform.position.y, targetPoint.position.z);

		Coroutines coroutines = new Coroutines();
		moveRoutine = StartCoroutine(coroutines.MoveRoutine(transform, destination, 1, SitDown));
	}

	/// <summary>
	/// 의자에 앉는 동작
	/// </summary>
	private void SitDown()
	{
		animator.SetTrigger("Sit");

		curCustomer.wait.WaitTime = 10;
		curCustomer.wait.onWait?.Invoke(curCustomer); //대기 시작
	}


	private void Exit(Customer cust)
	{
		animator.SetTrigger("Stand");
		//new WaitForSeconds(3); //todo.정확하게 다 일어난 순간을 캐치해야해. 

		animator.SetTrigger("BackSide");

		Coroutines coroutines = new Coroutines();
		StartCoroutine(coroutines.MoveRoutine(transform, info.Chair.StopPoint.position, 1, StandUp));
	}

	private void StandUp()
	{
		animator.SetTrigger("BackMove");

		Vector3 exitPoint = GameObject.FindGameObjectWithTag("CustEntryPoint").transform.position;

		Coroutines coroutines = new Coroutines();
		StartCoroutine(coroutines.MoveRoutine(meshAgent, transform, exitPoint, ExitStore));
	}

	private void ExitStore()
	{
		SeatManager.GetInstance().ReturnSeat(info.Seat);
		Destroy(gameObject);
	}
}
