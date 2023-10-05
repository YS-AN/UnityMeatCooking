using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "FurnData", menuName = "Data/Furniture")]
public class FurnData : ScriptableObject
{
	/// <summary>
	/// 가구 이름
	/// </summary>
	public FurnitureName Name;

	/// <summary>
	/// 가구 이미지
	/// </summary>
	public Sprite Icon;

	/// <summary>
	/// 가구 Object
	/// </summary>
	public GameObject FuncObject;

	/// <summary>
	/// 재료 가격
	/// </summary>
	public int Price;
}

