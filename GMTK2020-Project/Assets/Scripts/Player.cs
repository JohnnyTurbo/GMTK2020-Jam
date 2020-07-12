using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMG.GMTK2020 
{
    public class Player : Character
    {
		/*
		protected override void StateChanged(BattleState newState)
		{
			base.StateChanged(newState);
			switch (newState)
			{
				case BattleState.PlayerActionSelect:
					Debug.Log("Display UI");
					break;

				default:
					break;
			}
		}
		*/

		public override void CharacterDead()
		{
			base.CharacterDead();
			BattleController.instance.curVictoryStatus = VictoryStatus.Defeat;
		}
	}
}