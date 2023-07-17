using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum FurnitureName
{
	None = -1,
	Expansion = 0,
	HearthOven,
	TrashCan,

}

[CreateAssetMenu(fileName = "FurnData", menuName = "Data/Furniture")]
public class FurnData : ScriptableObject
{
	/// <summary>
	/// ���� �̸�
	/// </summary>
	public FurnitureName Name;

	/// <summary>
	/// ���� �̹���
	/// </summary>
	public Sprite Icon;

	/// <summary>
	/// ���� Object
	/// </summary>
	public GameObject FuncObject;

	/// <summary>
	/// ��� ����
	/// </summary>
	public int Price;
}

