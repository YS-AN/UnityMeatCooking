using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
	//���ӿ� �ʿ��� ���� ������Ʈ�� �̸� �����ص� -> FlyWeight ����
	Dictionary<string, Object> resources = new Dictionary<string, Object>();

	public T Load<T>(string path) where T : Object
	{
		string key = $"{typeof(T)}.{path}";//Ÿ�԰� ��θ� Ű������ ����� (Ű�� ������ ��������....)

		if(resources.ContainsKey(key)) //�̹� �ε��� �ߴٸ�? = �����͸� �־� �״ٸ�?
		{
			return resources[key] as T; //�ִ� �����͸� ��ȯ -> �ڿ� ����
		}
		else //�ε��� �����Ͱ� ���ٸ�?		
		{
			//�ϴ� ������ �ε��ϰ�, �ε��� �����͸� ��ȯ
			T resource = Resources.Load<T>(path);
			resources.Add(key, resource);
			return resource;
		}
	}

	public T Instantiate<T>(T original, Vector3 position, Quaternion rotation, bool pooling = false) where T : Object
	{
		if(pooling)
		{
			return GameManager.Pool.Get(original, position, rotation);
		}
		else
		{
			return Object.Instantiate(original, position, rotation);
		}
	}

	
	public T Instantiate<T>(string path, Vector3 position, Quaternion rotation, bool pooling = false) where T : Object
	{
		//��θ� �޾� �����͸� �������ų� ������ �� pooling �ý����� �����t
		T original = Load<T>(path);
		return Instantiate<T>(original, position, rotation, pooling); 
		
		// �ᱹ ������ �����͸�..... ���Ӱ� �����Ѵٴ� �ǹ� �Ƴ�? -> �淮�� �ǹ̰� �ֳ�...?
		// ���� �ҽ��� �������� �ϸ� ���Ӱ� ����������... 
		// ���߿� �ҽ��� ���� �� �̹����� �Ҹ� ���� �ڿ����� ���� �����ؼ� ��� �����ϵ��� ���� ����ؾ���. 
		// => �ܼ��� ���� ��� ���ظ� ���� �ҽ��� �����ϰ�, ���� ���� �ÿ��� ������ �� �ֵ��� �����
	}

	public T Instantiate<T>(string path, bool pooling = false) where T : Object
	{
		return Instantiate<T>(path, Vector3.zero, Quaternion.identity, pooling);
	}


	public void Destroy(GameObject obj)
	{
		if (GameManager.Pool.IsContain(obj))
		{
			//GameManager.Pool.Release�� true�� pooling�� ������Ʈ��. -> �ݳ���
			if (GameManager.Pool.Release(obj))
				return;
		}
	

		//Release�� false�� pooling�� �Ұ����ϱ� ������ �׳� ������
		GameObject.Destroy(obj);
	}
}
