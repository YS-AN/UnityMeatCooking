using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerSpawner : MonoBehaviour
{
	[SerializeField]
	Transform SpawnPoint;

	[SerializeField]
	private float SpawnTime;

	private Coroutine createEnemyRoutine;

	private void OnEnable()
	{
		createEnemyRoutine = StartCoroutine(SpawnRoutine());
	}

	private void OnDisable()
	{
		StopCoroutine(createEnemyRoutine);
	}

	IEnumerator SpawnRoutine()
	{
		while (true)
		{
			yield return new WaitForSeconds(SpawnTime);

			var seat = SeatManager.GetInstance().GetSeat();

			if(seat != null)
			{
				Customer newCust = CreateCustomer(seat);
				newCust.Mover?.OnEnter(newCust);
			}
		}
	}

	private Customer CreateCustomer(Seat seat)
	{
		var custPrefab = GameManager.Resource.Load<Customer>(Customer.ResourcesPath);

		var newCust = Instantiate(custPrefab, SpawnPoint.position, SpawnPoint.rotation);
		newCust.Mover.info.Init(seat, Random.Range(0, 2)); //todo.의자도 랜덤으로 앉기
		newCust.Wait.WaitTime = Random.Range(30, 41); //대기 시간은 30~40초 사이

		return newCust;
	}
}
