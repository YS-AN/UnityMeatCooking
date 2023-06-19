using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
	[SerializeField]
	private Transform stopPoint;
	public Transform StopPoint { get { return stopPoint; } }

	[SerializeField]
	private Transform[] seatPoints;
	public Transform[] SeatPoints { get { return seatPoints; } }
}
