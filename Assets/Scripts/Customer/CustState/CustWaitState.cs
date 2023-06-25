using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CustState
{
	public class CustWaitState : CustStatePattern<CustController>
	{
		private const string UI_PATH = "UI/Waiting";

		public CustWaitState(CustController owner, StateMachine<CustStateType, CustController> stateMachine)
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

		public override void Update()
		{

		}
	}
}