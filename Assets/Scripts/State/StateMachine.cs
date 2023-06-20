using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<TState, TOwner> where TOwner : MonoBehaviour
{
	private TOwner owner;
	private Dictionary<TState, StateBase<TState, TOwner>> states;
	private StateBase<TState, TOwner> curState;

	public StateMachine(TOwner owner)
	{
		this.owner = owner;
		this.states = new Dictionary<TState, StateBase<TState, TOwner>>();
	}

	public void AddState(TState state, StateBase<TState, TOwner> stateBase)
	{
		states.Add(state, stateBase);
	}

	/// <summary>
	/// ������ ��� ���¿� ���� �ʱ�ȭ �۾��� �����Ѵ�. (�� ������ SetUp�޼ҵ� ����)
	/// </summary>
	/// <param name="startState">SetUp�Ϸ� �� ó������ ������ ����</param>
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
		curState.Transition();
	}

	public void ChangeState(TState newState)
	{
		curState.Exit();
		curState = states[newState];
		curState.Enter();
	}
}
