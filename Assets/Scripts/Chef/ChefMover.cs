using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using static UnityEngine.GridBrushBase;

public class ChefMover : MonoBehaviour
{
	[SerializeField]
	private float walkSpeed;

	[SerializeField]
	private Transform player;

	private Animator animator;
	private CharacterController characterController;

	private Vector3 moveDir;
	private Vector3 prevMoveDir = Vector3.zero;

	private IActionable moveAction;
	private Coroutine moveRoutine;

	public bool IsMove { get; set; } = true;
	private float moveSpeed;

	private void Awake()
	{
		characterController = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
	} 

	private void Update()
	{
		if(IsMove)
			Move();
	}

	private void OnMove(InputValue value)
	{
		Vector2 input = value.Get<Vector2>();
		moveDir = new Vector3(input.x, 0, input.y);

		RotaionPlayer();
	}

	private void Move()
	{
		//Translate�� �ֺ��� ���ӻ�Ȳ�� ������� �ʰ�, ���� �浹��ü�� ������ �浹���� �ʰ� �Ѿ
		//CharacterController�� �ֺ���Ȳ�� ���� �� �� �ִ� ������ �� �� ���� ������ üũ�ϸ鼭 �ٴϴ� �̵� (�뷫�� ������ ��ǥ�� Ȱ����)  -> ex. ����̳� ��� ������ ���� �� �ֵ��� ��.

		//but. CharacterController�� �߷��� ������ ���� �ʾ�. �߷¿� ���� ó���� �ʿ��� -> y��ġ�� ĳ���� ������ ���� ������ �����.

		//characterController.Move(moveDir * moveSpeed * Time.deltaTime); //�ӷ� = ���ǵ� * Time.deltaTime(�����ð�)
		//�̷��� �����ϸ�, world�������� �����̱� ������ ȸ���� �� �̵��� �����.
		// ex.���� �¸� �����µ� ��� �����̴� ���� �߻���.

		moveSpeed = GetMoveSpeed(); //�����ӿ� ���� ������������ �ӵ��� �޾ƿ����� ��

		//������ �����θ� �̵��ϵ��� ���� (�ֳĸ� ������ �����ҰŶ�)
		characterController.Move(transform.forward * moveSpeed * Time.deltaTime);


		//*�ִϸ��̼� ����
		//dampTime, deltaTime�� ������ (Ư�� ���� �ִϸ��̼�) ��ȯ���� �ִϸ��̼��� �� ��� �ٲ� -> ���ڿ������� ����
		//	-> dampTime, deltaTime�� �־ �ִϸ��̼��� õõ��, ���׸Ӵ� ��ȯ�� �� �ֵ��� ������
		// dampTime : �ڿ������� ��ȯ �ӵ�
		// deltaTime : �ð�
		animator.SetFloat("XSpeed", moveDir.x, 0.1f, Time.deltaTime);
		animator.SetFloat("YSpeed", moveDir.z, 0.1f, Time.deltaTime);
		animator.SetFloat("MoveSpeed", moveSpeed);
	}


	private float GetMoveSpeed()
	{
		//Mathf.Lerp(���� ��ġ, ��ǥ�� �ϴ� ��ġ, ����ġ) 
		//	-> �������� : ���� ��ġ�� ��ǥ�� �ϴ� ��ġ ���̿� N%��(����ġ)�� ��ȯ��
		//		(����ġ�� ���� ���� ���� ������ ����)

		if (moveDir.magnitude == 0) //magnitude : ������ ũ�� -> ���� ũ�Ⱑ 0�̸� ������ ����
		{
			return Mathf.Lerp(moveSpeed, 0, 0.5f); // �� ������ ���� �ӵ��� ���� ���������� ��ǥ ���� 0���� ������ 
		}
		return Mathf.Lerp(moveSpeed, walkSpeed, 0.5f);
	}

	private void RotaionPlayer()
	{
		if (moveDir.magnitude > 0) //�������� ������ �����̴� �������� �÷��̾ ȸ��
		{
			Vector3 currentAngle = transform.rotation.eulerAngles;
			Vector3 rotationDirection = Vector3.zero;
			float rotationAngle = 0f;
			bool updateRotation = false;

			if (moveDir.x != 0 && prevMoveDir.x != moveDir.x)
			{
				rotationDirection = Vector3.up * moveDir.x;
				rotationAngle = 90 * moveDir.x;
				updateRotation = true;
			}
			else if (moveDir.z != 0 && prevMoveDir.z != moveDir.z)
			{
				rotationDirection = Vector3.forward * moveDir.y;
				rotationAngle = moveDir.z < 0 ? 180 : 0;
				updateRotation = true;
			}

			prevMoveDir = moveDir;

		

			if(updateRotation)
			{
				transform.localRotation = Quaternion.Euler(new Vector3(0, rotationAngle, 0));
			}
			
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		var obj = other.gameObject.GetComponent<IActionable>();

		if(obj != null)
		{
			obj.NextAction();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		var obj = other.GetComponent<IActionable>();

		if (obj != null)
		{
			obj.ClearAction();
		}
	}

}