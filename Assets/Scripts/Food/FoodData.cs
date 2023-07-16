using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "FoodData", menuName = "Data/Food")]
public class FoodData : ScriptableObject
{
	/// <summary>
	/// ���� �̸�
	/// </summary>
	public string Name;

	/// <summary>
	/// �����ð�
	/// </summary>
	public int CookingTime;

	/// <summary>
	/// ���� �̹���
	/// </summary>
	public Sprite Icon;

	/// <summary>
	/// ���� �� ������Ʈ
	/// </summary>
	public GameObject CookingObject;

	public string CookingObjectPath { get { return string.Format($"Food/{Name}/Cook_{Name}"); } }

	/// <summary>
	/// �ϼ� �� ������Ʈ
	/// </summary>
	public GameObject ResultObject;

	public string ResultObjectPath { get { return string.Format($"Food/{Name}/Dish_{Name}"); } }

	/// <summary>
	/// ���� ����
	/// </summary>
	public int Price;

	/// <summary>
	/// ���� ���� ����
	/// </summary>
	public bool IsLearn;

	/// <summary>
	/// ������
	/// </summary>
	public List<Recipe> Recipe;
}
