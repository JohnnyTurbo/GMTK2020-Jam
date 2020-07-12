using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMG.GMTK2020
{
    public abstract class Character : MonoBehaviour
    {
		public string charName;
		public int maxHealth;
		public int maxControl;
		public int actionAmount;
		public bool isControllable;
		public Action characterAction;
		public GameObject healthBarGO, controlBarGO, actionButtonGO;

		public Dictionary<string, Stat> charStats = new Dictionary<string, Stat>();

		private void Awake()
		{
			charStats.Add("Health", new Stat(maxHealth));
			charStats.Add("Control", new Stat(maxControl));
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
			switch (newState)
			{
				case BattleState.PreNewBattleSetup:
					BattleController.instance.RegisterCharacter(this);
					break;

				default:
					break;
			}
		}

		public void ModifyHealth(int amount)
		{
			charStats["Health"].ModifyCurStat(amount);
			UIController.instance.UpdateHealthUI(this);
			if(charStats["Health"].cur <= 0)
			{
				CharacterDead();
			}
		}

		private void CharacterDead()
		{
			Debug.Log("Character: " + gameObject.name + " ran out of health!");
			BattleController.instance.RemoveCharacter(this);
		}
	}
}