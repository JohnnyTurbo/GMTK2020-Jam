using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMG.GMTK2020
{
    public enum BattleState { PlayerActionSelect, PlayerActionPlayback, EnemyActionSelect, EnemyActionPlayback, None}

    public class BattleStateMachine : MonoBehaviour
    {
        public BattleState curBattleState;

        public void BeginBattle()
		{
            curBattleState = BattleState.PlayerActionSelect;
		}


    }
}