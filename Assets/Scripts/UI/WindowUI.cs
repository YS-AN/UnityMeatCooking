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
		transform.position += (Vector3)eventData.delta; //delta : 마우스 포인터의 변화량 
	}

	//마우스가 누르는 순간 반응하도록 함. 
	public void OnPointerDown(PointerEventData eventData)
	{
		//마우스가 누를 때 누른 윈도우 화면을 가장 상위에 위치하도록 변경함
		GameManager.UI.SelectWindowUI(this);
	}
}
