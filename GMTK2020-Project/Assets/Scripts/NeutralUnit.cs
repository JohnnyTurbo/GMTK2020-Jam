using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TMG.GMTK2020
{
    public class NeutralUnit : Character
    {
		public override void CharacterDead()
		{
			base.CharacterDead();
			Destroy(healthBarGO);
			UIController.instance.actionButtons.Remove(actionButtonGO.GetComponent<Button>());
			Destroy(actionButtonGO);
			Destroy(gameObject);
		}
	}
}