using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMG.GMTK2020 
{
	public enum HealthActionTypes { Attack, Heal}

    public class Action
    {
        public Character source, target;
		public string actionName;

        public virtual string Execute()
		{
			return null;
		}
    }

    public class HealthAction : Action
	{
		private int amount;
		private HealthActionTypes healthActionType;

		public HealthAction(Character _source, Character _target, int _amount, HealthActionTypes _healthActionType)
		{
			target = _target;
			source = _source;
			healthActionType = _healthActionType;
			switch (healthActionType)
			{
				case HealthActionTypes.Attack:
					amount = -_amount;
					actionName = "Attack";
					break;

				case HealthActionTypes.Heal:
					amount = _amount;
					actionName = "Heal";
					break;
			}
		}

		public override string Execute()
		{
			target?.ModifyHealth(source, amount);
			//Debug.Log("Source: " + source.gameObject.name + " Target: " + target.gameObject.name + " Amount: " + amount);
			return $"{source.charName} {actionName}ed {target.charName} for {Math.Abs(amount)} hit points!";
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