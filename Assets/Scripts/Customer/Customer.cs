using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
	//public const string ResourcesPath = "Prefabs/Cust_M1";

	public static string ResourcesPath { get { return GetCustomer(); } }

	private static string GetCustomer()
	{
		int num = Random.Range(1, 11);
		return string.Format("Customer/Cust_{0}", num);
	}

	public CustomerMover mover;
	public CustomerWait wait;

	private NavMeshAgent navAgent;

	private void Awake()
	{
		InitComponent();

		mover = GetComponent<CustomerMover>();
		wait = GetComponent<CustomerWait>();
		navAgent = GetComponent<NavMeshAgent>();
		navAgent.obstacleAvoidanceType = ObstacleAvoidanceType.MedQualityObstacleAvoidance;
		navAgent.areaMask = (1 << NavMesh.GetAreaFromName("CustWalkable"));
		//navAgent.autoRepath = true;
	}

	private void InitComponent()
	{
		this.AddComponent<Rigidbody>();
		this.AddComponent<NavMeshAgent>();
		this.AddComponent<CustomerMover>();
		this.AddComponent<CustomerWait>();
	}


}
