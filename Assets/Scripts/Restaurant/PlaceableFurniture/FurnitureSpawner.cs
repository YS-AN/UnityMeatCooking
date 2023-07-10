using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FurnitureSpawner : MonoBehaviour
{
	[SerializeField]
	Transform parentObj;

	public UnityAction<SaleItemData> OnCreateFurniture;

	private void Awake()
	{
		OnCreateFurniture += CreateFurniture;
	}

	private void CreateFurniture(SaleItemData item)
	{
		var obj = FurnitureManager.GetInstance().Furnitures[(FurnitureName)item.ItemId].FuncObject;
		var newFurn = GameManager.Resource.Instantiate(obj, transform.position, transform.rotation);
		newFurn.transform.SetParent(parentObj, true);
	}
}
