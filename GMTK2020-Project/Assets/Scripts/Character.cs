using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMG.GMTK2020
{
    public class Character : MonoBehaviour
    {
		public int maxHealth;
		public int maxControl;

		public Dictionary<string, Stat> playerStats = new Dictionary<string, Stat>();

		private void Awake()
		{
			playerStats.Add("Health", new Stat() { max = maxHealth, cur = maxHealth });
			playerStats.Add("Control", new Stat() { max = maxControl, cur = maxControl });

			
		}

		private void Start()
		{
			BattleStateMachine.instance.stateChanged += Instance_stateChanged;
		}

		private void Instance_stateChanged(object sender, BattleState newState)
		{
			StateChanged(newState);
		}

		protected virtual void StateChanged(BattleState newState)
		{

		}
	}
}