using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

//�丮 ��
//�ð� ��ŭ ����ϴٰ� �ϼ��� �丮�� ���� (�����̼� ����)
//�丮�� Ŭ���ϸ�, Dish�� �ٲ��..

public class Cook : MonoBehaviour
{
	[SerializeField]
	private Transform beingCookedFood;
	public Transform BeingCookedFood { get { return beingCookedFood; } }

	public FoodCooker Cooker;

	private void Awake()
	{
		InitComponent();
	}

	private void InitComponent()
	{
		this.AddComponent<FoodCooker>();
		Cooker = GetComponent<FoodCooker>();
	}
}
