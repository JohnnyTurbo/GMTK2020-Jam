using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMG.GMTK2020 
{
    public class Player : Character
    {

		public override void CharacterDead()
		{
			base.CharacterDead();
			BattleController.instance.curVictoryStatus = VictoryStatus.Defeat;
		}
	}
}