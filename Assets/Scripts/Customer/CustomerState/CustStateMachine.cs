using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustStateMachine<TState, TOwner> where TOwner : Customer
{
	private TOwner owner;
	
	/// <summary>
	/// 전체 상태를 가지고 있는 Dictionary
	/// </summary>
	private Dictionary<TState, StateBase<TState, TOwner>> states;

	/// <summary>
	/// 현재 상태
	/// </summary>
	private StateBase<TState, TOwner> curState;

	public CustStateMachine(TOwner owner)
	{
		this.owner = owner;
		this.states = new Dictionary<TState, StateBase<TState, TOwner>>();
	}

	public void AddState(TState state, StateBase<TState, TOwner> stateBase)
	{
		states.Add(state, stateBase);
	}

	/// <summary>
	/// 생성된 모든 상태에 대해 초기화 작업을 진행한다. (각 상태의 SetUp메소드 실행)
	/// </summary>
	/// <param name="startState">SetUp완료 후 처음으로 세팅할 상태</param>
	public void SetUp(TState startState)
	{
		foreach (var state in states.Values)
		{
			state.Setup();
		}

		curState = states[startState];
		curState.Enter();
	}

	public void Update()
	{
		curState.Update();
	}

	public void ChangeState(TState newState)
	{
		curState.Exit();
		curState = states[newState];
		curState.Enter();
	}
}
