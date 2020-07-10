using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMG.GMTK2020
{
    public enum BattleState { PlayerActionSelect, PlayerActionPlayback, EnemyActionSelect, EnemyActionPlayback, None}

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

		private void Start()
		{
			ChangeState(BattleState.PlayerActionSelect);
		}

		public void BeginBattle()
		{
            
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