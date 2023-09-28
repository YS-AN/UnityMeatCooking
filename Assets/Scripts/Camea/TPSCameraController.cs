using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class TPSCameraController : MonoBehaviour
{
	[SerializeField]
	private float mouseSensitivity;

	[SerializeField]
	private Transform cameraRoot;

	[SerializeField]
	private float Padding; //얼마나 테두리 가까이에 있을지를 정함

	private Vector2 lookDelta;
	private Vector3 moveDir;

	private float xRotation;
	private float yRotation;

	private float direction;

	public bool IsRotation;

	[SerializeField]
	private CinemachineVirtualCamera virtualCamera;
	private CinemachineTransposer transposer;

	private void Awake()
	{
		IsRotation = true;
	}

	private void Start()
	{
		//virtualCamera = GetComponent<CinemachineVirtualCamera>();
		transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
	}

	/*
	private void LateUpdate()
	{
		Pointer(direction);
	}

	private void OnLook(InputValue value)
	{
		//너무 어수선하니까 플레이어가 움직이지 않을 때만 방향 회전하도록 수정
		if (IsRotation)
			lookDelta = value.Get<Vector2>();
	}

	private void Look()
	{
		yRotation += lookDelta.x * mouseSensitivity * Time.deltaTime;
		//yRotation = Mathf.Clamp(yRotation, 180f, 360f);

		//xRotation -= lookDelta.y * mouseSensitivity * Time.deltaTime;
		//xRotation = Mathf.Clamp(xRotation, -80f, 80f); //x각도가 ~80도 ~ 80도까지만 움직이도록 막아줌

		cameraRoot.rotation = Quaternion.Euler(0, yRotation, 0);
	}

	private void OnPointer(InputValue value)
	{
		Vector2 mousePos = value.Get<Vector2>();
		direction = MovePosition(mousePos.x, Screen.width); //Screen.width : 스크린의 가로 크기
	}

	private float MovePosition(float mousePoint, int screenSize)
	{
		//x축이면 좌, 우
		//y축이면 위, 아래

		//마우스 위치가 테두리에 가까이 있을 때 움직이는 방식 -> Padding범위를 받아서 padding에 있을 때만 움직이도록 함

		if (mousePoint <= Padding) //마우스가 왼쪽이나 위쪽 거의 끝에 있을 떄
		{
			return -1; //왼쪽이나 위쪽으로 이동
		}
		else if (mousePoint >= screenSize - Padding) //마우스가 스크린 끝쪽 (오른쪽이나 아래쪽)에 있을 때
		{
			return 1; //오른쪽이나 아래쪽으로 이동
		}
		else //padding범위 밖으면 움직이지 않음
		{
			return 0;
		}

	}
	private void Pointer(float direction)
	{
		if (IsRotation == false || direction == 0 || GameManager.Data.IsPlaceable)
			return;

		PlayerManager.GetInstance().Player.transform.Rotate(Vector3.up * 100 * direction * Time.deltaTime, Space.World);
		//cameraRoot.Rotate(Vector3.up * 50 * direction * Time.deltaTime, Space.World);
	}
	*/
}