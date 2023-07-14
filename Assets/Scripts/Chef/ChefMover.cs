using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ChefMover : MonoBehaviour
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
		//Cursor.lockState = CursorLockMode.Confined; //���콺�� ����â�� ����� ���ϰ� ����

	}

	private void OnDisable()
	{

		//mouseClickActions.performed -= OnMove;

		Cursor.lockState = CursorLockMode.None;
	}

	private void OnMove(InputValue value)
	{
		if (GameManager.Data.IsPlaceable)
			return;

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

			Move();
		}
	}

	private void Move()
	{
		//StartCoroutine(PlayerManager.GetInstance().Player.Camera.RotateCamera());

		moveRoutine = StartCoroutine(MoveRoutine(moveAction));
	}
	private IEnumerator MoveRoutine(IMoveable moveable)
	{
		SetMoveAction(true);

		Vector3 target = moveable.StopPoint.position;
		meshAgent.destination = target;

		//y���� ����
		float playerDistancetoFloor = transform.position.y - target.y;
		target.y += playerDistancetoFloor;

		//todo.Distance -> ��Ʈ������ ���� ������ ���ӿ� ������ ��. -> ����ȭ �ʿ�
		while (Vector3.Distance(transform.position, target) > 0.1f)
		{
			yield return null;
		}
		SetMoveAction(false);
		//TurnToDestination(moveable.StopPoint);

		moveable.NextAction();
	}

	/*
	private Vector3 GetDistance(Vector3 start, Vector3 target)
	{
		Vector3 difference = target - start;
		float squaredDistance = Vector3.Dot(difference, difference); //���� �Ÿ� ���ϱ�

		float distance = Mathf.Sqrt(squaredDistance);
	}
	*/

	private void SetMoveAction(bool isMoveAction)
	{
		meshAgent.ResetPath(); 

		isMove = isMoveAction;
		animator.SetBool("IsMove", isMove);

		PlayerManager.GetInstance().Player.Camera.IsRotation = !isMove;
	}

	private void TurnToDestination(Transform stopPostion)
	{
		Coroutines coroutines = new Coroutines();
		StartCoroutine(coroutines.LocalBasedRotationRoutine(transform, Quaternion.Euler(0, stopPostion.rotation.y, 0), 2f));
	}
}

	//GraduallyRotateRoutine