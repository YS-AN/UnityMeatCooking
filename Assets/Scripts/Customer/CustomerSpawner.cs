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
				newCust.Mover?.OnEnter();
			}
		}
	}

	private Customer CreateCustomer(Seat seat)
	{
		var custPrefab = GameManager.Resource.Load<Customer>(Customer.ResourcesPath);

		var newCust = Instantiate(custPrefab, SpawnPoint.position, SpawnPoint.rotation);
		newCust.CurState = CustStateType.Enter;
		newCust.Mover.info.Init(seat, Random.Range(0, 2)); //todo.ÀÇÀÚµµ ·£´ýÀ¸·Î ¾É±â
		return newCust;
	}
}
