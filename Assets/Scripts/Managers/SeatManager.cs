using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatManager : MonoBehaviour
{
	private static SeatManager instance;

	public static SeatManager GetInstance()
	{
		if(instance == null)
			instance = new SeatManager();

		return instance;
	}

	private Stack<Seat> seats;

	private void Awake()
	{
		instance = this;

		seats = new Stack<Seat>();
		var installedSeats = GetComponentsInChildren<Seat>();

		foreach(var seat in installedSeats)
		{
			seats.Push(seat);
		}
	}

	public int Count { get { return seats.Count; } }

	public void ReturnSeat(Seat seat)
	{
		seats.Push(seat);
	}

	public Seat GetSeat()
	{
		if(seats.Count > 0)
		{
			return seats.Pop();
		}
		return null;
	}
}
