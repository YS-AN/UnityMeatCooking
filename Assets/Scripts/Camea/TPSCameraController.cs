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
	private float Padding; //�󸶳� �׵θ� �����̿� �������� ����

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
		//�ʹ� ������ϴϱ� �÷��̾ �������� ���� ���� ���� ȸ���ϵ��� ����
		if (IsRotation)
			lookDelta = value.Get<Vector2>();
	}

	private void Look()
	{
		yRotation += lookDelta.x * mouseSensitivity * Time.deltaTime;
		//yRotation = Mathf.Clamp(yRotation, 180f, 360f);

		//xRotation -= lookDelta.y * mouseSensitivity * Time.deltaTime;
		//xRotation = Mathf.Clamp(xRotation, -80f, 80f); //x������ ~80�� ~ 80�������� �����̵��� ������

		cameraRoot.rotation = Quaternion.Euler(0, yRotation, 0);
	}

	private void OnPointer(InputValue value)
	{
		Vector2 mousePos = value.Get<Vector2>();
		direction = MovePosition(mousePos.x, Screen.width); //Screen.width : ��ũ���� ���� ũ��
	}

	private float MovePosition(float mousePoint, int screenSize)
	{
		//x���̸� ��, ��
		//y���̸� ��, �Ʒ�

		//���콺 ��ġ�� �׵θ��� ������ ���� �� �����̴� ��� -> Padding������ �޾Ƽ� padding�� ���� ���� �����̵��� ��

		if (mousePoint <= Padding) //���콺�� �����̳� ���� ���� ���� ���� ��
		{
			return -1; //�����̳� �������� �̵�
		}
		else if (mousePoint >= screenSize - Padding) //���콺�� ��ũ�� ���� (�������̳� �Ʒ���)�� ���� ��
		{
			return 1; //�������̳� �Ʒ������� �̵�
		}
		else //padding���� ������ �������� ����
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