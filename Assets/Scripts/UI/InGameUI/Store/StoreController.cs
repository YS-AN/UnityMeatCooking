using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class StoreController : MonoBehaviour
{
	protected StoreModel model;

	public StoreController(StoreModel model)
	{
		this.model = model;
	}

	/// <summary>
	/// store UI ����Ʈ ����� �¾�
	/// </summary>
	public virtual void SetSalesContent()
	{
		model.SalesItems = GetSaleItemDatas();
		model.Items = model.SaleContent.GetComponentsInChildren<SaleItem>().ToList();

		AdjustSalesList();

		//PlayerManager.GetInstance().Player.Camera.IsRotation = false;
	}

	/// <summary>
	/// store UI ����Ʈ ����� ������ �°� ����
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
	/// store UI ����Ʈ�� �� ������ ��������
	/// </summary>
	protected abstract List<SaleItemData> GetSaleItemDatas();

	/// <summary>
	/// store UI ����Ʈ �ʿ��� ��ŭ ����
	/// </summary>
	/// <param name="createdCnt"></param>
	private void InitSaleContent(int createdCnt)
	{
		for (int i = 0; i < createdCnt; i++)
			CreateItem();

		model.Items = model.SaleContent.GetComponentsInChildren<SaleItem>().ToList();
	}

	/// <summary>
	/// ����Ʈ �ȿ� ���� ������Ʈ ����
	/// </summary>
	private void CreateItem()
	{
		var newItem = Instantiate(model.SaleItemPrefab, Vector3.zero, Quaternion.identity);
		newItem.transform.SetParent(model.SaleContent);
	}

	/// <summary>
	/// ����Ʈ ���� ����
	/// </summary>
	public virtual void ClearSalesContent()
	{
		//PlayerManager.GetInstance().Player.Camera.IsRotation = true;
	}
}