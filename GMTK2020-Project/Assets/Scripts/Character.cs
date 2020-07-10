using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMG.GMTK2020
{
    public class Character : MonoBehaviour
    {
		public int maxHealth;
		public int maxControl;
		public int actionAmount;
		public Action characterAction;

		public Dictionary<string, Stat> charStats = new Dictionary<string, Stat>();

		private void Awake()
		{
			charStats.Add("Health", new Stat() { max = maxHealth, cur = maxHealth });
			charStats.Add("Control", new Stat() { max = maxControl, cur = maxControl });
			SetupAction();
		}

		private void Start()
		{
			BattleStateMachine.instance.stateChanged += Instance_stateChanged;
		}

		private void Instance_stateChanged(object sender, BattleState newState)
		{
			StateChanged(newState);
		}

		protected virtual void SetupAction()
		{
			characterAction = new HealthAction(this, null, actionAmount, "Attack");
		}

		protected virtual void StateChanged(BattleState newState)
		{

		}

		public void ModifyHealth(int amount)
		{
			charStats["Health"].cur += amount;
			if(charStats["Health"].cur >= charStats["Health"].max)
			{
				charStats["Health"].cur = charStats["Health"].max;
			}
			else if(charStats["Health"].cur <= 0)
			{
				CharacterDead();
			}
		}

		private void CharacterDead()
		{
			Debug.Log("Character: " + gameObject.name + " ran out of health!");
		}
	}
}