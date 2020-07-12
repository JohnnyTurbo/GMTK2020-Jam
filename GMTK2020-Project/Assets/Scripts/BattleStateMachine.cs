using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMG.GMTK2020
{
    public enum BattleState {
								PreNewBattleSetup,
								NewBattleSetup,
								SetupPlayerTurn,
								PlayerActionSelect,
								PlayerActionTargetSelect,
								PrePlayerActionPlayback,
								PlayerActionPlayback,
								EnemyActionSelect,
								PreEnemyActionPlayback,
								EnemyActionPlayback,
								Cutscene,
								None
							}

    public class BattleStateMachine : MonoBehaviour
    {
        public static BattleStateMachine instance;

        public BattleState curBattleState { get; private set; }

		public event EventHandler<BattleState> stateChanged;

		private void Awake()
		{
			instance = this;
			curBattleState = BattleState.None;			
		}

		public void ChangeState(BattleState newState)
		{
			if(curBattleState != newState)
			{
				stateChanged?.Invoke(this, newState);
				curBattleState = newState;
			}
		}
    }
}