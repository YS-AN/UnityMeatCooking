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
	/// ���� ���� �� �ൿ -> ������Ʈ �ʱ�ȭ
	/// </summary>
	public abstract void TakeActionAfterNoti();
}
