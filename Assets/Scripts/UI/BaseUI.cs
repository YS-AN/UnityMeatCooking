using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// ��� UI�� �⺻�� �� Ŭ���� 
///  -> ��� UI�� �������� ��Ҹ� ������ ����
/// </summary>
public class BaseUI : MonoBehaviour
{
    protected Dictionary<string, RectTransform> transforms; // UI�� transform�� ��ӹ��� RectTransform�� �����.
    protected Dictionary<string, Button> buttons;
    protected Dictionary<string, TMP_Text> texts;
    
    protected virtual void Awake()
    {
        BindChildren();
    }

	private void BindChildren()
	{
        transforms = new Dictionary<string, RectTransform>();
        buttons = new Dictionary<string, Button>(); 
        texts = new Dictionary<string, TMP_Text>();

		RectTransform[] children = GetComponentsInChildren<RectTransform>();

		foreach (var child in children)
        {
            string key = child.gameObject.name;

            if (transforms.ContainsKey(key)) 
                continue; 

            transforms.Add(key, child);

			BindDictionary(child, buttons);
			BindDictionary(child, texts);
		}
	}

    private void BindDictionary<T> (RectTransform child, Dictionary<string, T> dic)
    {
		T obj = child.GetComponent<T>();
		if (obj != null)
			dic.Add(child.gameObject.name, obj);
	}
}
