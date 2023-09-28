using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seat : MonoBehaviour
{
	private Chair[] _chairs;
	public Chair[] Chairs { get {  return _chairs; } }

	private bool _isRight;
	private bool IsRight { get { return _isRight; } }

	private void Awake()
	{
		_chairs = GetComponentsInChildren<Chair>();

		/*
		if (transform.rotation.y == 1)
		{
			foreach(var chair in _chairs)
			{
				chair.StopPoint.transform.eulerAngles = new Vector3(0, 0, 0);

				foreach(var point in chair.SeatPoints)
				{
					point.transform.eulerAngles = new Vector3(0, 180, 0);
				}
			}
		}
		//*/
	}

	public void CleanTable()
	{
		foreach(var chair in _chairs)
		{
			foreach(var point in chair.SeatPoints)
			{
				var food = point.GetComponentInChildren<FoodPoint>().GetComponentInChildren<Dish>();

				if(food != null)
				{
					Destroy(food);
				}
			}
		}
	}
}
