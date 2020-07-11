using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMG.GMTK2020 
{
    public class Action
    {
        public Character source, target;
		public string actionName;

        public virtual void Execute()
		{

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

		public override void Execute()
		{
			target?.ModifyHealth(amount);
			//Debug.Log("Source: " + source.gameObject.name + " Target: " + target.gameObject.name + " Amount: " + amount);
		}
	}
}