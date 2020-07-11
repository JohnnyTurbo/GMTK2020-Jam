using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TMG.GMTK2020
{
	public class UIController : MonoBehaviour
	{
		public GameObject actionButtonPrefab;
		public Canvas HUDCanvas;

		private void Start()
		{
			DisplayBattleActions();
		}

		public void DisplayBattleActions()
		{
			foreach (Character curChar in BattleController.instance.battleCharacters)
			{
				GameObject newActionButtonGO = GameObject.Instantiate(actionButtonPrefab);
				newActionButtonGO.transform.parent = HUDCanvas.transform;
				Button newActionButton = newActionButtonGO.GetComponent<Button>();
				TextMeshProUGUI newActionButtonText = newActionButtonGO.GetComponentInChildren<TextMeshProUGUI>();
				newActionButtonText.text = curChar.characterAction.actionName;
			}
		}
	}
}
