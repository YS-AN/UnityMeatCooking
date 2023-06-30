using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Coroutines
{
	public IEnumerator MoveRoutine(Transform mover, Vector3 destination, float speed, UnityAction OnNextAction = null)
	{
		Vector3 startPosition = mover.position;
		float distance = Vector3.Distance(startPosition, destination);
		float totalTime = distance / speed;
		float elapsedTime = 0f;

		while (elapsedTime < totalTime)
		{
			float time = elapsedTime / totalTime;
			mover.position = Vector3.Lerp(startPosition, destination, time);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		mover.position = destination; //위치 조정

		OnNextAction?.Invoke();
	}

	public IEnumerator MoveRoutine(NavMeshAgent meshAgent, Transform mover, Vector3 destination, UnityAction OnNextAction = null)
	{
		meshAgent.SetDestination(destination);

		while (true)
		{
			if (Vector3.Distance(destination, mover.position) < 0.1f)
			{
				break;
			}
			yield return null;
		}

		meshAgent.isStopped = true;
		meshAgent.ResetPath();  //네비게이션 목적지 제거

		OnNextAction?.Invoke();
	}


	public IEnumerator JustWaitRoutine(float waitTime, UnityAction OnNextAction = null)
	{
		yield return new WaitForSeconds(waitTime);

		OnNextAction?.Invoke();
	}


	public IEnumerator OpenDoorRoutine(Transform door, Quaternion targetRotation, float duration = 3f, UnityAction OnNextAction = null)
	{
		Quaternion startRotation = door.localRotation;

		float elapsedTime = 0f;

		while (elapsedTime < duration)
		{
			float time = elapsedTime / duration;
			door.localRotation = Quaternion.Lerp(startRotation, targetRotation, time);

			elapsedTime += Time.deltaTime;
			yield return null;
		}
		door.localRotation = targetRotation;

		OnNextAction?.Invoke();
	}
}
