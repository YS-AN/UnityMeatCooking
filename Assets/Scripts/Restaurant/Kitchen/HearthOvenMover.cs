using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearthOvenMover : Placeable
{
	public override void EndDragAction()
	{
		var oven = transform.GetComponent<HearthOven>();
		oven.StopPoint = oven.StopPosition.position;
	}
}
