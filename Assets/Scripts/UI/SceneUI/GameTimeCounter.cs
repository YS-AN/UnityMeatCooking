using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameTimeCounter : MonoBehaviour
{
	[SerializeField]
	private TMP_Text txtGameTime;

	[SerializeField]
	private float totalTime = 300f; //5분

	[SerializeField]
	private float lastOrderTime = 30f;

	[SerializeField]
	private CustomerSpawner customers;

	[SerializeField]
	private TimeOutUI tiemoutUI;

	private float gameTime = 0.1f;

	public UnityAction OnStartGameTimeCount;

	private void Awake()
	{
		OnStartGameTimeCount += SetStartTimeCount;
	}

	private void SetStartTimeCount()
	{
		StartCoroutine(TimeCounterRoutine());
	}

	private IEnumerator TimeCounterRoutine()
	{
		gameTime = totalTime;

		while (gameTime > 0f)
		{
			if(gameTime == lastOrderTime)
			{
				txtGameTime.color = Color.red;
				GameManager.Data.IsOpenrestaurant = false;
			}

			// 시간 형식으로 변환하여 텍스트에 표시
			int minutes = Mathf.FloorToInt(gameTime / 60f);
			int seconds = Mathf.FloorToInt(gameTime % 60f);
			txtGameTime.text = string.Format("{0:D2}:{1:D2}", minutes, seconds);

			yield return new WaitForSeconds(1);
			gameTime--;
		}
		txtGameTime.text = "00:00";

		ClosedStore();

		tiemoutUI.gameObject.SetActive(true);
		tiemoutUI.OnContinueGame += ResetTime;
	}

	private void ClosedStore()
	{
		var custs = customers.GetComponentsInChildren<Customer>();
		foreach(var cust in custs)
		{
			cust.Mover.OnExit?.Invoke();
		}
		PlayerManager.GetInstance().SetStartPosition();
	}

	private void ResetTime()
	{
		Debug.Log("ResetTime");

		txtGameTime.color = Color.white;

		int minutes = Mathf.FloorToInt(totalTime / 60f);
		int seconds = Mathf.FloorToInt(totalTime % 60f);
		txtGameTime.text = string.Format("{0:D2}:{1:D2}", minutes, seconds); 
		
		StartCoroutine(TimeCounterRoutine());
	}
}
