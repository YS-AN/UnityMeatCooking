using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable
{
	public Vector3 StopPoint { get; set; }

	public void NextAction();

	public void ClearAction();
}
