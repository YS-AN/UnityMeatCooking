using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class HearthOvenMover : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	private Vector3 initialPosition;
	private Vector3 offset;

	private void Start()
	{
		initialPosition = transform.position;
	}

	public void OnDrag(PointerEventData eventData)
	{
		Vector3 currentPosition = GetMouseWorldPosition();
		offset = currentPosition - initialPosition;
		offset.y = 0f; // y 축은 고정하여 수직 이동 방지

		transform.position = initialPosition + offset;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		var oven = transform.GetComponent<HearthOven>();
		oven.StopPoint = oven.stopPosition.position;
	}

	private Vector3 GetMouseWorldPosition()
	{
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.z = Camera.main.WorldToScreenPoint(initialPosition).z;
		return Camera.main.ScreenToWorldPoint(mousePosition);
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
	}
}
