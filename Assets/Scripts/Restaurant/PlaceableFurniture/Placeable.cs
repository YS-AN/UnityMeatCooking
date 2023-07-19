using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Placeable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	[SerializeField]
	protected Shadow shadow;
	
	private MeshRenderer meshRenderer;

	/// <summary>
	/// 드래그 시작 위치
	/// </summary>
	private Vector3 initialPosition;

	private Vector3 offset;

	private void Awake()
	{
		meshRenderer = shadow.GetComponent<MeshRenderer>();
		shadow.gameObject.SetActive(false);
	}

	private void Start()
	{
		initialPosition = transform.position;
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		shadow.gameObject.SetActive(true);
		meshRenderer.materials[0].color = Color.black;
	}

	public void OnDrag(PointerEventData eventData)
	{
		if(GameManager.Data.IsPlaceable)
		{
			Vector3 currentPosition = GetMouseWorldPosition();
			offset = currentPosition - initialPosition;
			offset.y = 2f; // y 축은 고정하여 수직 이동 방지

			transform.position = initialPosition + offset;
			shadow.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
		}
	}

	private Vector3 GetMouseWorldPosition()
	{
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.z = Camera.main.WorldToScreenPoint(initialPosition).z;
		return Camera.main.ScreenToWorldPoint(mousePosition);
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		transform.transform.position = shadow.IsEnterOffLimits ? initialPosition : new Vector3(transform.position.x, 0, transform.position.z);
		shadow.OnPlacedOffLimits?.Invoke();

		shadow.gameObject.SetActive(false);

		EndDragAction();
	}

	public abstract void EndDragAction();
	
}
