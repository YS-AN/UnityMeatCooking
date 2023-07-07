using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Store : InGameUI
{
	const string UI_PATH = "UI/SaleItem";

	[SerializeField]
	private Transform SaleContent;

	[SerializeField]
	private SaleItem SaleItemPrefab;

	private StoreController uiController;

	protected override void Awake()
	{
		base.Awake();
		buttons["BtnClose"].onClick.AddListener(() => { CloseStore(); });
	}

	private void OnEnable()
	{
		StoreModel model = new StoreModel(SaleItemPrefab, SaleContent);
		uiController = GameManager.Data.IsPlaceable ? new FurnStoreController(model) : new IngrStoreController(model);
		uiController.SetSalesContent();
	}

	private void OnDisable()
	{
		uiController.ClearSalesContent();
	}

	private void CloseStore()
	{
		GameManager.UI.CloseInGameUI(this);
	}
}


public class StoreModel
{
	public StoreModel(SaleItem item, Transform saleContent)
	{
		SaleContent = saleContent;
		SaleItemPrefab = item;
		Items = new List<SaleItem>();
	}

	/// <summary>
	/// store ui list content object
	/// </summary>
	public Transform SaleContent { get; set; }

	/// <summary>
	/// list object 
	/// </summary>
	public SaleItem SaleItemPrefab;

	/// <summary>
	/// list data
	/// </summary>
	public List<SaleItem> Items { get; set; }

	/// <summary>
	/// setting data
	/// </summary>
	public List<SaleItemData> SalesItems { get; set; }
}

public abstract class StoreController : MonoBehaviour
{
	protected StoreModel model;

	public StoreController(StoreModel model)
	{
		this.model = model;
	}

	/// <summary>
	/// store UI 리스트 목록을 셋업
	/// </summary>
	public virtual void SetSalesContent()
	{
		model.SalesItems = GetSaleItemDatas();
		model.Items = model.SaleContent.GetComponentsInChildren<SaleItem>().ToList();

		AdjustSalesList();

		PlayerManager.GetInstance().Player.Camera.IsRotation = false;
	}

	/// <summary>
	/// store UI 리스트 목록을 개수에 맞게 정리
	/// </summary>
	protected void AdjustSalesList()
	{
		if (model.Items.Count < model.SalesItems.Count)
			InitSaleContent(model.SalesItems.Count - model.Items.Count);

		else if (model.Items.Count > model.SalesItems.Count)
		{
			int count = model.Items.Count - model.SalesItems.Count;
			int max = model.Items.Count - 1;

			for (int i = 0; i < count; i++)
				Destroy(model.Items[max - i].gameObject);

			model.Items = model.Items.Take(model.SalesItems.Count).ToList();
		}
	}

	/// <summary>
	/// store UI 리스트에 들어갈 데이터 가져오기
	/// </summary>
	protected abstract List<SaleItemData> GetSaleItemDatas();

	/// <summary>
	/// store UI 리스트 필요한 만큼 생성
	/// </summary>
	/// <param name="createdCnt"></param>
	private void InitSaleContent(int createdCnt)
	{
		for (int i = 0; i < createdCnt; i++)
			CreateItem();

		model.Items = model.SaleContent.GetComponentsInChildren<SaleItem>().ToList();
	}

	/// <summary>
	/// 리스트 안에 내부 오브젝트 생성
	/// </summary>
	private void CreateItem()
	{
		var newItem = Instantiate(model.SaleItemPrefab, Vector3.zero, Quaternion.identity);
		newItem.transform.SetParent(model.SaleContent);
	}

	/// <summary>
	/// 리스트 내용 정리
	/// </summary>
	public virtual void ClearSalesContent()
	{
		PlayerManager.GetInstance().Player.Camera.IsRotation = true;
	}
}

public class IngrStoreController : StoreController
{
	public IngrStoreController(StoreModel model) : base(model) { }

	protected override List<SaleItemData> GetSaleItemDatas()
	{
		List<SaleItemData> items = new List<SaleItemData>();

		foreach (var item in StorageManager.GetInstance().Ingredients)
		{
			SaleItemData data = new SaleItemData();

			data.ItemId = (int)item.Key;
			data.ItemIcon = item.Value.Data.Icon;
			data.ItemPrice = item.Value.Data.Price;

			items.Add(data);
		}

		return items;
	}

	public override void SetSalesContent()
	{
		base.SetSalesContent();

		int index = 0;
		foreach (var item in model.Items)
		{
			item.SaleData = model.SalesItems[index++];
			item.BtnBuy.transform.GetComponent<Buyer>().OnBuy += BuyItem;
		}
	}

	/// <summary>
	/// 아이템 구매 시 동작
	/// </summary>
	/// <param name="item"></param>
	private void BuyItem(SaleItemData item)
	{
		if (StorageManager.GetInstance().Ingredients.ContainsKey((IngredientName)item.ItemId))
			StorageManager.GetInstance().Ingredients[(IngredientName)item.ItemId].Count++;
	}

	public override void ClearSalesContent()
	{
		base.ClearSalesContent();

		foreach (var item in model.Items)
		{
			item.BtnBuy.transform.GetComponent<Buyer>().OnBuy -= BuyItem;
		}
	}
}

public class FurnStoreController : StoreController
{
	public FurnStoreController(StoreModel model) : base(model) { }

	protected override List<SaleItemData> GetSaleItemDatas()
	{
		List<SaleItemData> items = new List<SaleItemData>();

		foreach (var item in FurnitureManager.GetInstance().Furnitures)
		{
			SaleItemData data = new SaleItemData();

			data.ItemId = (int)item.Key;
			data.ItemIcon = item.Value.Icon;
			data.ItemPrice = item.Value.Price;

			items.Add(data);
		}
		return items;
	}

	public override void SetSalesContent()
	{
		base.SetSalesContent();

		int index = 0;
		foreach (var item in model.Items)
		{
			item.SaleData = model.SalesItems[index++];
			item.BtnBuy.transform.GetComponent<Buyer>().OnBuy += BuyItem;
		}
	}

	/// <summary>
	/// 아이템 구매 시 동작
	/// </summary>
	/// <param name="item"></param>
	private void BuyItem(SaleItemData item)
	{
		//오브젝트 생성
	}

	public override void ClearSalesContent()
	{
		base.ClearSalesContent();

		foreach (var item in model.Items)
		{
			item.BtnBuy.transform.GetComponent<Buyer>().OnBuy -= BuyItem;
		}
	}

}

