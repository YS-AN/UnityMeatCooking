using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : BaseUI
{
	public Transform followTarget;

	public Vector2 followOffset; //기준점과 얼마나 떨어진 위치에 만들지 정하는 백터 (떨어진 거리를 저장함)



	private void LateUpdate()
	{
		SetFollow();
	}

	public void SetTarget(Transform target)
	{
		//설정 하자마자 해당 UI 위치로 바로 이동할수 있도록 세팅함

		this.followTarget = target;

		if (followTarget != null)
		{
			transform.position = Camera.main.WorldToScreenPoint(followTarget.position) + (Vector3)followOffset;
		}
	}

	public void SetOffset(Vector2 offset)
	{
		this.followOffset = offset;
	}

	private void SetFollow()
	{
		if (followTarget == null)  //따라다닐 오브젝트가 없으면? 
		{
			//todo. ui삭제해주기
		}

		else
		{
			//UI는 해상도를 기준으로 함 -> 일반 게임 오브젝트와 UI의 transform이 완전 다름 .
			//WorldToScreenPoint : 게임 월드 기준에서의 위치
			//  => ui의 transform을 게임 오브젝트의 transform의 형태로 변환을 해줌
			transform.position = Camera.main.WorldToScreenPoint(followTarget.position) + (Vector3)followOffset;
		}
	}
}
