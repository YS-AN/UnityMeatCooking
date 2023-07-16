using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "FoodData", menuName = "Data/Food")]
public class FoodData : ScriptableObject
{
	/// <summary>
	/// 음식 이름
	/// </summary>
	public string Name;

	/// <summary>
	/// 조리시간
	/// </summary>
	public int CookingTime;

	/// <summary>
	/// 음식 이미지
	/// </summary>
	public Sprite Icon;

	/// <summary>
	/// 조리 중 오브젝트
	/// </summary>
	public GameObject CookingObject;

	public string CookingObjectPath { get { return string.Format($"Food/{Name}/Cook_{Name}"); } }

	/// <summary>
	/// 완성 된 오브젝트
	/// </summary>
	public GameObject ResultObject;

	public string ResultObjectPath { get { return string.Format($"Food/{Name}/Dish_{Name}"); } }

	/// <summary>
	/// 음식 가격
	/// </summary>
	public int Price;

	/// <summary>
	/// 조리 가능 여부
	/// </summary>
	public bool IsLearn;

	/// <summary>
	/// 레시피
	/// </summary>
	public List<Recipe> Recipe;
}
