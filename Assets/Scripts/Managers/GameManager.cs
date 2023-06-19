using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEditor.EditorTools;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private static GameManager instance;
	private static ResourceManager resourceManager;
	private static PoolManager poolManager;

	public static GameManager Instance { get { return instance; } }
	public static ResourceManager Resource { get { return resourceManager; } }
	public static PoolManager Pool { get { return poolManager; } }

	private void Awake()
	{
		if (instance != null)
		{
			Destroy(this);
			return;
		}

		instance = this;
		DontDestroyOnLoad(this);
		InitManagers();
	}

	private void OnDestroy()
	{
		if (instance == this)
			instance = null;
	}

	private void InitManagers()
	{
		GameObject resourceObj = new GameObject();
		resourceObj.name = "ResourceManager";
		resourceObj.transform.parent = transform;
		resourceManager = resourceObj.AddComponent<ResourceManager>();

		GameObject poolObj = new GameObject();
		poolObj.name = "PoolManager";
		poolObj.transform.parent = transform;
		poolManager = poolObj.AddComponent<PoolManager>();
	}
}