using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffLimitsRange : MonoBehaviour
{
	[SerializeField]
	private LayerMask ShadowLayer;

	[SerializeField]
	private LayerMask FloorLayer;

	/// <summary>
	/// Shadow box
	/// </summary>
	[SerializeField]
	private GameObject exceptionObj;

	private void Update()
	{
		IsGround();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (exceptionObj != null && other.gameObject == exceptionObj)
			return;

		if (ShadowLayer.IsContain(other.gameObject.layer))
		{
			SetShadowEnterValue(other.gameObject.GetComponent<Shadow>(), true);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (ShadowLayer.IsContain(other.gameObject.layer))
		{
			SetShadowEnterValue(other.gameObject.GetComponent<Shadow>(), false);
		}
	}

	private void SetShadowEnterValue(Shadow shadow, bool isEnter)
	{
		if(shadow != null)
		{
			shadow.IsEnterOffLimits = isEnter;
		}
	}

	private void IsGround()
	{
		float groundDistance = 5f;

		bool isGrounded = Physics.BoxCast(transform.position, Vector3.one, Vector3.down, out var hit, Quaternion.identity, groundDistance, FloorLayer);

		Debug.DrawRay(transform.position, Vector3.down * groundDistance, Color.red);

		if (exceptionObj != null)
		{
			Shadow shadow = exceptionObj.gameObject.GetComponent<Shadow>();
			if (shadow != null)
				shadow.IsOutsideStore = !isGrounded;
		}
	}
}
