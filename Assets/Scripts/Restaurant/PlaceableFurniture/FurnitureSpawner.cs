using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FurnitureSpawner : MonoBehaviour, IEndable
{
	public UnityAction<SaleItemData> OnCreateFurniture;
	public UnityAction OnShopExpansion;

	public void TakeActionAfterNoti()
	{
		
	}

	private void Awake()
	{
		OnCreateFurniture += CreateFurniture;
		OnShopExpansion += ShopExpansion;
	}

	/// <summary>
	/// 가구 생성
	/// </summary>
	/// <param name="item"></param>
	private void CreateFurniture(SaleItemData item)
	{
		var obj = FurnitureManager.GetInstance().Furnitures[(FurnitureName)item.ItemId].FuncObject;
		var newFurn = GameManager.Resource.Instantiate(obj, transform.position, transform.rotation);
		newFurn.transform.SetParent(transform, true);
	}

	/// <summary>
	/// 매장 확장
	/// </summary>
	private void ShopExpansion()
	{
		if(FurnitureManager.GetInstance().Furnitures[FurnitureName.Expansion].Price < 160000)
		{
			FurnitureManager.GetInstance().Furnitures[FurnitureName.Expansion].Price *= 2;


		}
			
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
