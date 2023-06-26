using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
	[SerializeField]
	private float MoveSpeed;

	private NavMeshAgent meshAgent;
	private Animator animator;
	private CharacterController characterController;

	private Vector3 moveDir;

	private IMoveable moveAction;
	private Coroutine moveRoutine;

	private bool isMove;

	private void Awake()
	{
		characterController = GetComponent<CharacterController>();
		meshAgent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();

		isMove = false;
	}

	private void OnEnable()
	{
		//Cursor.lockState = CursorLockMode.Confined; //마우스가 게임창을 벗어나지 못하게 막음

	}

	private void OnDisable()
	{

		//mouseClickActions.performed -= OnMove;

		Cursor.lockState = CursorLockMode.None;
	}

	private void Update()
	{
		
	}

	private void OnMove(InputValue value)
	{
		var ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
		if (Physics.Raycast(ray: ray, hitInfo: out var hit) && hit.collider)
		{
			if (moveRoutine != null)
				StopCoroutine(moveRoutine);

			if (moveAction != null)
				moveAction.ClearAction();

			moveAction = hit.transform.GetComponent<IMoveable>();
			if (moveAction == null)
				return;

			moveRoutine = StartCoroutine(MoveRoutine(moveAction));
		}
	}

	private IEnumerator MoveRoutine(IMoveable moveable)
	{
		SetMoveAction(true);

		Vector3 target = moveable.StopPoint;
		meshAgent.destination = target;

		//y축은 고정
		float playerDistancetoFloor = transform.position.y - target.y;
		target.y += playerDistancetoFloor;

		while (Vector3.Distance(transform.position, target) > 0.1f)
		{
			yield return null;
		}
		SetMoveAction(false);

		moveable.NextAction();
	}

	private void SetMoveAction(bool isMoveAction)
	{
		isMove = isMoveAction;
		animator.SetBool("IsMove", isMove);
	}
}
