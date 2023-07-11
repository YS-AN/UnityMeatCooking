using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FurnitureSpawner : MonoBehaviour, IEndable
{
	public UnityAction<SaleItemData> OnCreateFurniture;

	public void TakeActionAfterNoti()
	{
		throw new System.NotImplementedException();
	}

	private void Awake()
	{
		OnCreateFurniture += CreateFurniture;
	}

	private void CreateFurniture(SaleItemData item)
	{
		var obj = FurnitureManager.GetInstance().Furnitures[(FurnitureName)item.ItemId].FuncObject;
		var newFurn = GameManager.Resource.Instantiate(obj, transform.position, transform.rotation);
		newFurn.transform.SetParent(transform, true);
	}

	public void InitObject()
	{
		var CreatedFurnitures = transform.GetComponentsInChildren<GameObject>();

		foreach(var obj in CreatedFurnitures)
		{
			Destroy(obj);
		}	
	}
}
