using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seat : MonoBehaviour
{
	private Chair[] _chairs;
	public Chair[] Chairs { get {  return _chairs; } }

	private Table _table;
	public Table Table { get { return _table; } }

	private bool _isRight;
	private bool IsRight { get { return _isRight; } }

	private void Awake()
	{
		_chairs = GetComponentsInChildren<Chair>();
		_table = GetComponentInChildren<Table>();

		_isRight = transform.rotation.y == 1;
	}
}
