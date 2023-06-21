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
				var custPrefab = GameManager.Resource.Load<Customer>(Customer.ResourcesPath);

				var newCust = Instantiate(custPrefab, SpawnPoint.position, SpawnPoint.rotation);
				newCust.mover.info.Init(seat, 0);
				newCust.mover?.OnEnter(newCust);
			}
		}
	}
}
