using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamController : MonoBehaviour
{
	[SerializeField]
	private Transform followCamPostion;

	private void Update()
	{
		transform.position = followCamPostion.position;
	}
}
