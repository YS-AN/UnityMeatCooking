using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffLimitsRange : MonoBehaviour
{
	[SerializeField]
	private LayerMask ShadowLayer;

	/// <summary>
	/// ÀÚ½ÅÀÇ Shadow box
	/// </summary>
	[SerializeField]
	private GameObject exceptionObj;

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
}
