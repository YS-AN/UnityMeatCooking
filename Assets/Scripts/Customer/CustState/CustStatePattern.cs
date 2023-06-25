using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class CustStatePattern<T> : StateBase<CustStateType, T> where T : MonoBehaviour
{
	protected GameObject gameObject => owner.gameObject;
	protected Transform transform => owner.transform;
	//protected Rigidbody2D rigidbody => owner.Rigidbody;
	//protected SpriteRenderer renderer => owner.SpriteRnder;
	//protected Animator animator => owner.Animator;
	//protected Collider2D collider => owner.Collider;

	/// <summary>
	/// player transform
	/// </summary>
	protected Transform target;

	protected CustStatePattern(T owner, StateMachine<CustStateType, T> stateMachine)
		: base(owner, stateMachine)
	{
	}
}