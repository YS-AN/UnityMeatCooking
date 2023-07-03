using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AddClicker : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public Transform TargetPoint;

	public Transform MoveObject;

	public void OnBeginDrag(PointerEventData eventData)
	{
		throw new System.NotImplementedException();
	}

	public void OnDrag(PointerEventData eventData)
	{
		throw new System.NotImplementedException();
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		throw new System.NotImplementedException();
	}
}

public class AddPocket : AddClicker
{
	
}