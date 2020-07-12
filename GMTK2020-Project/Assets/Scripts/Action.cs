using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMG.GMTK2020 
{
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

		public HealthAction(Character _source, Character _target, int _amount, string _actionName)
		{
			amount = _amount;
			target = _target;
			source = _source;
			actionName = _actionName;
		}

		public override string Execute()
		{
			target?.ModifyHealth(amount);
			Debug.Log("Source: " + source.gameObject.name + " Target: " + target.gameObject.name + " Amount: " + amount);
			return $"{source.charName} {actionName}ed {target.charName} for {amount} hit points!";
		}
	}
}