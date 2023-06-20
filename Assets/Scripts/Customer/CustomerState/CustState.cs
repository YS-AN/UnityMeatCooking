using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CustomerState
{
	public class WaitState : CustStatePattern<CustController>
	{
		public WaitState(CustController owner, StateMachine<CustStateType, CustController> stateMachine)
			: base(owner, stateMachine)
		{
		}

		public override void Setup()
		{
		
		}

		public override void Enter()
		{
			
		}

		public override void Exit()
		{
		}

		public override void Transition()
		{
		
		}

		public override void Update()
		{
		}
	}
}