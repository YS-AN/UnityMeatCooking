using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
	public static string ResourcesPath { get { return GetCustomer(); } }

	private static string GetCustomer()
	{
		int num = Random.Range(1, 11);
		return string.Format("Customer/Cust_{0}", num);
	}

	public CustStateType CurState;

	private NavMeshAgent navAgent;
	private CapsuleCollider capsuleCollider;
	private BoxCollider boxCollider;

	public CustomerMover Mover;
	public CustomerWait Wait;
	public CustomerOrder Order;
	public CustomerEater Eater;
	public CustomerExit Exit;
	public CustomerRater Rater;
	public CustomerStatus Status;

	public CustomerInfo CustInfo;

	private void Awake()
	{
		InitComponent();
	}

	private void InitComponent()
	{
		this.AddComponent<NavMeshAgent>();
		this.AddComponent<CapsuleCollider>();
		this.AddComponent<BoxCollider>();

		this.AddComponent<CustomerMover>();
		this.AddComponent<CustomerWait>();
		this.AddComponent<CustomerOrder>();
		this.AddComponent<CustomerEater>();
		this.AddComponent<CustomerExit>();
		this.AddComponent<CustomerRater>();
		this.AddComponent<CustomerStatus>();

		SetComponent();
	}

	private void SetComponent()
	{
		navAgent = transform.GetComponent<NavMeshAgent>();
		navAgent.obstacleAvoidanceType = ObstacleAvoidanceType.MedQualityObstacleAvoidance;
		navAgent.areaMask = (1 << NavMesh.GetAreaFromName("CustWalkable"));

		boxCollider = transform.GetComponent<BoxCollider>();
		boxCollider.isTrigger = false;
		boxCollider.center = new Vector3(0, 0.9f, 0);
		boxCollider.size = new Vector3(1.5f, 2.3f, 0.3f);

		capsuleCollider = transform.GetComponent<CapsuleCollider>();
		capsuleCollider.isTrigger = true;
		capsuleCollider.center = new Vector3(0, 1, 0);
		capsuleCollider.radius = 1f;
		capsuleCollider.height = 2;

		Mover = transform.GetComponent<CustomerMover>();
		Wait = transform.GetComponent<CustomerWait>();
		Order = transform.GetComponent<CustomerOrder>();
		Eater = transform.GetComponent<CustomerEater>();
		Exit = transform.GetComponent<CustomerExit>();
		Rater = transform.GetComponent<CustomerRater>();
		Status = transform.GetComponent<CustomerStatus>();
	}
}
