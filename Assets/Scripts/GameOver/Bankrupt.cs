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
	/// 게임 오버인지 확인
	/// </summary>
	/// <returns></returns>
	private bool IsGameOver()
	{
		//먹고 있는 손님이 없음 = 계산할 손님이 없음 = 더 이상 들어올 돈이 없음
		var custList = Customers.GetComponentsInChildren<Customer>();
		if (custList.Count() > 0 && custList.Where(x => x.CurState == CustStateType.Eating).Count() != 0)
		{
			int curMoney = GameManager.Data.Revenue;
			//현재 들어온 주문을 남은 재고로 처리가 가능한가?
			return !IsPossibleCook(GameManager.Data.Revenue);
		}
		return false;
	}

	/// <summary>
	/// 현재 재고로 주문 받은 요리가 가능한지 확인
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