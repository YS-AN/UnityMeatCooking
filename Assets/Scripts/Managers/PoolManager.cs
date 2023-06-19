using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool; //����Ƽ�������� ������Ʈ Ǯ�� ������ ��. (2021���� ��������)
using UnityEngine.UIElements;

public class PoolManager : MonoBehaviour
{
	Dictionary<string, ObjectPool<GameObject>> poolDic; 

	private void Awake()
	{
		poolDic = new Dictionary<string, ObjectPool<GameObject>>();
	}
	
	//TODO. boxing, unboxing ����ؼ� ����ȭ �غ���

	public T Get<T>(T original, Vector3 position, Quaternion rotation) where T : Object
	{
		if (original is GameObject)
		{
			GameObject prefab = original as GameObject;

			if (poolDic.ContainsKey(prefab.name) == false) //�ѹ��� ������Ʈ Ǯ�� ��������� �ʴٸ�? 
			{
				CreatePool(prefab.name, prefab); //������Ʈ Ǯ�� ����� ��
			}

			GameObject obj = poolDic[prefab.name].Get();

			obj.transform.position = position;
			obj.transform.rotation = rotation;
			return obj as T;
		}
		else if (original is Component)
		{
			Component component = original as Component;
			string key = component.gameObject.name;

			if (poolDic.ContainsKey(key) == false)
			{
				CreatePool(key, component.gameObject); //������Ʈ Ǯ�� ����� ��
			}

			GameObject obj = poolDic[key].Get();

			obj.transform.position = position;
			obj.transform.rotation = rotation;
			return obj.GetComponent<T>();

		}
		else //�� �ܿ��� null�� return��
		{
			return null;
		}
	}

	public T Get<T>(T original) where T : Object
	{
		return Get(original, Vector3.zero, Quaternion.identity);
	}

	private void CreatePool(string key, GameObject prefab)
	{
		ObjectPool<GameObject> pool = new ObjectPool<GameObject>(
			createFunc: () =>
			{
				GameObject obj = Instantiate(prefab);
				obj.name = key;
				return obj;
			},
			actionOnGet: (GameObject obj) =>
			{
				obj.SetActive(true);
				obj.transform.parent = null;

			},
			actionOnRelease: (GameObject obj) =>
			{
				obj.SetActive(false);
				obj.transform.SetParent(transform);
			},
			actionOnDestroy: (GameObject obj) =>
			{
				Destroy(obj);
			}
		);

		poolDic.Add(prefab.name, pool); 
	}

	public bool Release(GameObject prefab)
	{
		if (poolDic.ContainsKey(prefab.name) == false) //�ݳ��� key�� ���ٸ�? 
		{
			return false; //�ݳ� ����
		}

		poolDic[prefab.name].Release(prefab);
		return true;
	}

	public bool IsContain<T>(T original) where T : Object
	{
		if (original is GameObject)
		{
			GameObject prefab = original as GameObject;
			return poolDic.ContainsKey(prefab.name);
		}
		else if (original is Component)
		{
			Component component = original as Component;
			return poolDic.ContainsKey(component.gameObject.name);
		}
		return false;
	}
}