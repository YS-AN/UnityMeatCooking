using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : NotifyContorller<IEndable>
{
	protected override void Awake()
	{
		base.Awake();
	}
}


public abstract class EndController : MonoBehaviour, IEndable
{
	protected GameOver gameOver;

	protected virtual void Start()
	{
		gameOver = GetComponent<GameOver>();
		gameOver.AddObserver(this);
	}

	/// <summary>
	/// 게임 종료 시 행동 -> 오브젝트 초기화
	/// </summary>
	public abstract void TakeActionAfterNoti();
}
