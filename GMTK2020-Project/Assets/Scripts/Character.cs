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
		public int controlCost;
		public bool isControllable;
		public Action characterAction;
		public GameObject healthBarGO, controlBarGO, actionButtonGO;
		public HealthActionTypes actionType;

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
			characterAction = new HealthAction(this, null, null, actionType);
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

		public void ModifyControl(int amount)
		{
			//Debug.Log($"Modifying control by {amount}");
			charStats["Control"].ModifyCurStat(amount);
			UIController.instance.UpdateControlUI(this);			
		}

		public void ModifyHealth(Character source, int amount)
		{
			charStats["Health"].ModifyCurStat(amount);
			UIController.instance.UpdateHealthUI(this);			
			if(charStats["Health"].cur <= 0)
			{
				DeathAction deathAction = new DeathAction(source, this);
				BattleController.instance.AddActionToFront(deathAction);
			}			
		}

		public virtual void CharacterDead()
		{
			BattleController.instance.RemoveCharacter(this);
		}
	}
}