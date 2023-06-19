using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool; //유니티엔진에서 오브젝트 풀을 지원해 줌. (2021부터 지원해줌)
using UnityEngine.UIElements;

public class PoolManager : MonoBehaviour
{
	Dictionary<string, ObjectPool<GameObject>> poolDic; 

	private void Awake()
	{
		poolDic = new Dictionary<string, ObjectPool<GameObject>>();
	}
	
	//TODO. boxing, unboxing 고려해서 최적화 해보기

	public T Get<T>(T original, Vector3 position, Quaternion rotation) where T : Object
	{
		if (original is GameObject)
		{
			GameObject prefab = original as GameObject;

			if (poolDic.ContainsKey(prefab.name) == false) //한번도 오브젝트 풀이 만들어지지 않다면? 
			{
				CreatePool(prefab.name, prefab); //오브젝트 풀을 만들어 줌
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
				CreatePool(key, component.gameObject); //오브젝트 풀을 만들어 줌
			}

			GameObject obj = poolDic[key].Get();

			obj.transform.position = position;
			obj.transform.rotation = rotation;
			return obj.GetComponent<T>();

		}
		else //그 외에는 null을 return함
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
		if (poolDic.ContainsKey(prefab.name) == false) //반납할 key가 없다면? 
		{
			return false; //반납 실패
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