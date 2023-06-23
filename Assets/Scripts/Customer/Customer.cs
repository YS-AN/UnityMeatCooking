using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour//, IMoveable
{
	public static string ResourcesPath { get { return GetCustomer(); } }

	private static string GetCustomer()
	{
		int num = Random.Range(1, 11);
		return string.Format("Customer/Cust_{0}", num);
	}

	private NavMeshAgent navAgent;
	private CapsuleCollider capsuleCollider;

	public CustomerOrder Order;
	public CustomerMover Mover;
	public CustomerWait Wait;

	public CustomerInfo CustInfo;

	private void Awake()
	{
		InitComponent();
	}

	private void InitComponent()
	{
		this.AddComponent<NavMeshAgent>();
		this.AddComponent<CapsuleCollider>();

		this.AddComponent<CustomerMover>();
		this.AddComponent<CustomerWait>();
		this.AddComponent<CustomerOrder>();

		SetComponent();
	}

	private void SetComponent()
	{
		navAgent = GetComponent<NavMeshAgent>();
		navAgent.obstacleAvoidanceType = ObstacleAvoidanceType.MedQualityObstacleAvoidance;
		navAgent.areaMask = (1 << NavMesh.GetAreaFromName("CustWalkable"));

		capsuleCollider = GetComponent<CapsuleCollider>();
		capsuleCollider.isTrigger = true;
		capsuleCollider.center = new Vector3(0, 1, 0);
		capsuleCollider.radius = 0.3f;
		capsuleCollider.height = 2;

		Mover = GetComponent<CustomerMover>();
		Wait = GetComponent<CustomerWait>();
		Order = GetComponent<CustomerOrder>();
	}
}
