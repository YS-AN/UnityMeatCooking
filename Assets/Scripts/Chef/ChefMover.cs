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
		//Translate는 주변에 게임상황을 고려히지 않고, 정적 충돌에체는 무조건 충돌하지 않고 넘어감
		//CharacterController는 주변상황에 의해 갈 수 있는 지형과 갈 수 없는 지형을 체크하면서 다니는 이동 (대략적 물리적 지표를 활용함)  -> ex. 계단이나 언덕 정도는 오를 수 있도록 함.

		//but. CharacterController는 중력을 가지고 있지 않아. 중력에 대한 처리가 필요함 -> y위치를 캐릭터 설정에 따라 움직여 줘야해.

		//characterController.Move(moveDir * moveSpeed * Time.deltaTime); //속력 = 스피드 * Time.deltaTime(단위시간)
		//이렇게 구현하면, world기준으로 움직이기 때문에 회전한 후 이동이 어색함.
		// ex.내가 좌를 눌렀는데 우로 움직이는 현상 발생함.

		moveSpeed = GetMoveSpeed(); //움직임에 따라 선형보간으로 속도를 받아오도록 함

		//무조건 앞으로만 이동하도록 수정 (왜냐면 방향을 변경할거라서)
		characterController.Move(transform.forward * moveSpeed * Time.deltaTime);


		//*애니메이션 설정
		//dampTime, deltaTime이 없으면 (특히 블랜더 애니메이션) 전환에서 애니메이션이 그 즉시 바뀜 -> 부자연스러워 보임
		//	-> dampTime, deltaTime을 넣어서 애니메이션이 천천히, 슬그머니 전환될 수 있도록 유도함
		// dampTime : 자연스러운 전환 속도
		// deltaTime : 시간
		animator.SetFloat("XSpeed", moveDir.x, 0.1f, Time.deltaTime);
		animator.SetFloat("YSpeed", moveDir.z, 0.1f, Time.deltaTime);
		animator.SetFloat("MoveSpeed", moveSpeed);
	}


	private float GetMoveSpeed()
	{
		//Mathf.Lerp(현재 수치, 목표로 하는 수치, 가중치) 
		//	-> 선형보간 : 현재 수치와 목표로 하는 수치 사이에 N%값(가중치)을 반환함
		//		(가중치가 높을 수록 값이 빠르게 변함)

		if (moveDir.magnitude == 0) //magnitude : 백터의 크기 -> 백터 크기가 0이면 움직임 없음
		{
			return Mathf.Lerp(moveSpeed, 0, 0.5f); // 안 움직일 때는 속도가 점점 떨어지도록 목표 값을 0으로 세팅함 
		}
		return Mathf.Lerp(moveSpeed, walkSpeed, 0.5f);
	}

	private void RotaionPlayer()
	{
		if (moveDir.magnitude > 0) //움직임이 있으면 움직이는 방향으로 플레이어를 회전
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