using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActionable
{
	public Transform StopPoint { get; set; }

	public void NextAction();

	public void ClearAction();
}
