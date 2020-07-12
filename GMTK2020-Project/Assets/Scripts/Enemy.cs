using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TMG.GMTK2020
{
	public class Enemy : Character
	{
		protected override void StateChanged(BattleState newState)
		{
			base.StateChanged(newState);
			switch (newState)
			{
				case BattleState.EnemyActionSelect:
					//Debug.Log("show enemy selction UI");
					SelectAction();
					break;

				default:
					break;
			}
		}

		private void SelectAction()
		{
			if(BattleController.instance.battleCharacters.FirstOrDefault(c => c is Player)is Player foundP)
			{
				characterAction.target = foundP;
				//Debug.Log("Found player");
			}
			BattleController.instance.AddAction(characterAction);
			//BattleStateMachine.instance.ChangeState(BattleState.EnemyActionPlayback);
		}

		public override void CharacterDead()
		{
			base.CharacterDead();
			BattleController.instance.curVictoryStatus = VictoryStatus.Victory;
		}
	}
}