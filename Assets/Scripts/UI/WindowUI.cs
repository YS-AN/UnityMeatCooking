using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WindowUI : BaseUI, IDragHandler, IPointerDownHandler
{
	protected override void Awake()
	{
		base.Awake();
		
		buttons["BtnClose"].onClick.AddListener(() => { GameManager.UI.CloseWindowUI<WindowUI>(this); });
	}


	public void OnDrag(PointerEventData eventData)
	{
		transform.position += (Vector3)eventData.delta; //delta : ���콺 �������� ��ȭ�� 
	}

	//���콺�� ������ ���� �����ϵ��� ��. 
	public void OnPointerDown(PointerEventData eventData)
	{
		//���콺�� ���� �� ���� ������ ȭ���� ���� ������ ��ġ�ϵ��� ������
		GameManager.UI.SelectWindowUI(this);
	}
}
