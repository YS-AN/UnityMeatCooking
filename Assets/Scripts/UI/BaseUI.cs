using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// 모든 UI의 기본이 될 클래스 
///  -> 모든 UI의 공통적인 요소를 가지고 있음
/// </summary>
public class BaseUI : MonoBehaviour
{
    protected Dictionary<string, RectTransform> transforms; // UI는 transform을 상속받은 RectTransform을 사용함.
    protected Dictionary<string, Button> buttons;
    protected Dictionary<string, TMP_Text> texts;
	protected Dictionary<string, Image> images;
	protected Dictionary<string, Slider> sliders;

	protected virtual void Awake()
    {
        BindChildren();
    }

	private void BindChildren()
	{
        transforms = new Dictionary<string, RectTransform>();
        buttons = new Dictionary<string, Button>(); 
        texts = new Dictionary<string, TMP_Text>();
		images = new Dictionary<string, Image>();
		sliders = new Dictionary<string, Slider>();

		RectTransform[] children = GetComponentsInChildren<RectTransform>();

		foreach (var child in children)
        {
            string key = child.gameObject.name;

            if (transforms.ContainsKey(key)) 
                continue; 

            transforms.Add(key, child);

			BindDictionary(child, buttons);
			BindDictionary(child, texts);
            BindDictionary(child, images);
			BindDictionary(child, sliders);
		}
	}

    private void BindDictionary<T> (RectTransform child, Dictionary<string, T> dic)
    {
		T obj = child.GetComponent<T>();
		if (obj != null)
			dic.Add(child.gameObject.name, obj);
	}
}
