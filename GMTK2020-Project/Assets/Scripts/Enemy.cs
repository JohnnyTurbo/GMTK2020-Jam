using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMG.GMTK2020
{
	public class Enemy : Character
	{
		protected override void StateChanged(BattleState newState)
		{
			switch (newState)
			{
				case BattleState.EnemyActionSelect:
					Debug.Log("show enemy selction UI");
					break;

				default:
					break;
			}
		}
	}
}