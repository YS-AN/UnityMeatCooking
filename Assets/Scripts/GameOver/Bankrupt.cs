using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Bankrupt : MonoBehaviour
{
	private const string UI_PATH = "UI/GameOverUI";
	private const string CustListTag = "CustSpawner";

	GameOverUI gameOverUI = null;
	public UnityAction OnBankrupt;

	private Transform Customers;

	public void Awake()
	{
		OnBankrupt += OpenGameOverUI;

	}

	public void Start()
	{
		Customers = GameObject.FindGameObjectWithTag(CustListTag).transform;
	}

	private void Update()
	{
		if(IsGameOver())
		{
			OpenGameOverUI();
		}
	}

	/// <summary>
	/// ���� �������� Ȯ��
	/// </summary>
	/// <returns></returns>
	private bool IsGameOver()
	{
		//�԰� �ִ� �մ��� ���� = ����� �մ��� ���� = �� �̻� ���� ���� ����
		var custList = Customers.GetComponentsInChildren<Customer>();
		if (custList.Count() > 0 && custList.Where(x => x.CurState == CustStateType.Eating).Count() != 0)
		{
			int curMoney = GameManager.Data.Revenue;
			//���� ���� �ֹ��� ���� ���� ó���� �����Ѱ�?
			return !IsPossibleCook(GameManager.Data.Revenue);
		}
		return false;
	}

	/// <summary>
	/// ���� ���� �ֹ� ���� �丮�� �������� Ȯ��
	/// </summary>
	/// <returns></returns>
	private bool IsPossibleCook(int curMoney)
	{
		var orderList = FoodManager.GetInstance().GetOrderList();
		var havingList = StorageManager.GetInstance().HavingList;
		var ingredientPrices = StorageManager.GetInstance().Ingredients;

		var cookedIngredients = orderList.Values.SelectMany(order => order.FoodInfo.Recipe)
			.GroupBy(g => g.Name)
			.Select(x => new { Name = x.Key, Cnt = x.Count() });


		foreach (var item in cookedIngredients)
		{
			if (havingList.TryGetValue(item.Name, out var availableList))
			{
				if (item.Cnt > availableList.Count)
				{
					int missingCount = item.Cnt - availableList.Count;
					int ingredientPrice = ingredientPrices[item.Name].Data.Price;
					curMoney -= ingredientPrice * missingCount;
				}
			}
			else
			{
				if (item.Cnt > ingredientPrices[item.Name].Count)
				{
					int ingredientPrice = ingredientPrices[item.Name].Data.Price;
					curMoney -= ingredientPrice * orderList.Count;
				}
			}

			if (curMoney < 0)
			{
				return false;
			}
		}
		return true;
	}

	private void OpenGameOverUI()
	{
		PlayerManager.GetInstance().Player.Animator.SetTrigger("IsDown");

		gameOverUI = GameManager.UI.ShowInGameUI<GameOverUI>(UI_PATH);

		InitGameData();
	}

	private void InitGameData()
	{
		FoodManager.GetInstance().RemoveAllOrder();
	}

}