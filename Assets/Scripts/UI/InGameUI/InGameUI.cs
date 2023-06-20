using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : BaseUI
{
	public Transform followTarget;

	public Vector2 followOffset; //�������� �󸶳� ������ ��ġ�� ������ ���ϴ� ���� (������ �Ÿ��� ������)



	private void LateUpdate()
	{
		SetFollow();
	}

	public void SetTarget(Transform target)
	{
		//���� ���ڸ��� �ش� UI ��ġ�� �ٷ� �̵��Ҽ� �ֵ��� ������

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
		if (followTarget == null)  //����ٴ� ������Ʈ�� ������? 
		{
			//todo. ui�������ֱ�
		}

		else
		{
			//UI�� �ػ󵵸� �������� �� -> �Ϲ� ���� ������Ʈ�� UI�� transform�� ���� �ٸ� .
			//WorldToScreenPoint : ���� ���� ���ؿ����� ��ġ
			//  => ui�� transform�� ���� ������Ʈ�� transform�� ���·� ��ȯ�� ����
			transform.position = Camera.main.WorldToScreenPoint(followTarget.position) + (Vector3)followOffset;
		}
	}
}
