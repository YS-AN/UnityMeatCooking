using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
	//게임에 필요한 공유 오브젝트를 미리 저장해둠 -> FlyWeight 적용
	Dictionary<string, Object> resources = new Dictionary<string, Object>();

	public T Load<T>(string path) where T : Object
	{
		string key = $"{typeof(T)}.{path}";//타입과 경로를 키값으로 사용함 (키값 정도는 취향차이....)

		if(resources.ContainsKey(key)) //이미 로딩을 했다면? = 데이터를 넣어 뒀다면?
		{
			return resources[key] as T; //있는 데이터를 반환 -> 자원 공유
		}
		else //로딩할 데이터가 없다면?		
		{
			//일단 데이터 로딩하고, 로딩한 데이터를 반환
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
		//경로를 받아 데이터를 가져오거나 생성한 후 pooling 시스템을 적용햠
		T original = Load<T>(path);
		return Instantiate<T>(original, position, rotation, pooling); 
		
		// 결국 공유된 데이터를..... 새롭게 생성한다는 의미 아냐? -> 경량의 의미가 있나...?
		// 지금 소스를 기준으로 하면 새롭게 생성되지만... 
		// 나중에 소스를 만들 때 이미지나 소리 같은 자원들을 따로 공유해서 사용 가능하도록 만들어서 사용해야함. 
		// => 단순히 동작 방식 이해를 위한 소스로 생각하고, 실제 개발 시에는 공유할 수 있도록 만들기
	}

	public T Instantiate<T>(string path, bool pooling = false) where T : Object
	{
		return Instantiate<T>(path, Vector3.zero, Quaternion.identity, pooling);
	}


	public void Destroy(GameObject obj)
	{
		if (GameManager.Pool.IsContain(obj))
		{
			//GameManager.Pool.Release가 true면 pooling한 오브젝트임. -> 반납함
			if (GameManager.Pool.Release(obj))
				return;
		}
	

		//Release가 false면 pooling이 불가능하기 때문에 그냥 삭제함
		GameObject.Destroy(obj);
	}
}
