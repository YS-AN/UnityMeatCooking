using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TPSCameraController : MonoBehaviour
{
	[SerializeField]
	private float mouseSensitivity;

	[SerializeField]
	private Transform cameraRoot;

	private Vector2 lookDelta;
	private float xRotation;
	private float yRotation;

	private void LateUpdate()
	{
		Look();
	}

	private void OnLook(InputValue value)
	{
		lookDelta = value.Get<Vector2>();
	}

	private void Look()
	{
		yRotation += lookDelta.x * mouseSensitivity * Time.deltaTime;

		//xRotation -= lookDelta.y * mouseSensitivity * Time.deltaTime;
		//xRotation = Mathf.Clamp(xRotation, -80f, 80f); //x������ ~80�� ~ 80�������� �����̵��� ������

		cameraRoot.rotation = Quaternion.Euler(0, yRotation, 0);
	}
}
