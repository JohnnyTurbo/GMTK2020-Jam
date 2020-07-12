using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMG.GMTK2020 
{
	public enum HealthActionTypes { Attack, Heal}

    public class Action
    {
        public Character source, target, controller;
		public int amount;
		public string actionName;

        public virtual string Execute()
		{
			return null;
		}

		public virtual string ExecuteMissed()
		{
			return null;
		}
    }

    public class HealthAction : Action
	{

		private int controlCost;
		private HealthActionTypes healthActionType;

		public HealthAction(Character _source, Character _target, Character _controller, HealthActionTypes _healthActionType)
		{
			target = _target;
			source = _source;
			controlCost = source.controlCost;
			healthActionType = _healthActionType;
			switch (healthActionType)
			{
				case HealthActionTypes.Attack:
					amount = -source.actionAmount;
					actionName = "Attack";
					break;

				case HealthActionTypes.Heal:
					amount = source.actionAmount;
					actionName = "Heal";
					break;
			}
		}

		public override string Execute()
		{
			target?.ModifyHealth(source, amount);
			controller?.ModifyControl(-source.controlCost);
			//Debug.Log("Source: " + source.gameObject.name + " Target: " + target.gameObject.name + " Amount: " + amount);
			return $"{source.charName} {actionName}ed {target.charName} for {Math.Abs(amount)} hit points!";
		}

		public override string ExecuteMissed()
		{
			return $"{source.charName} tried to {actionName} {target.charName} but it missed!";
		}
	}

	public class DeathAction : Action
	{
		public DeathAction(Character _source, Character _target)
		{
			source = _source;
			target = _target;
		}

		public override string Execute()
		{
			target?.CharacterDead();
			string deadStr = $"{target.charName} ran out of health";
			return deadStr;
		}
	}
}