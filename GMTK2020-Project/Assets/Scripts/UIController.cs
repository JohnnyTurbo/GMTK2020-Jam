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
		public Canvas worldCanvas;

		private void Start()
		{
			BattleStateMachine.instance.stateChanged += Instance_stateChanged;
		}

		private void Instance_stateChanged(object sender, BattleState newState)
		{
			switch (newState)
			{
				case BattleState.PlayerActionSelect:
					DisplayBattleActions();
					break;

				default:
					break;
			}
		}



		public void DisplayBattleActions()
		{
			foreach (Character curChar in BattleController.instance.battleCharacters)
			{
				if (!curChar.isControllable) { continue; }
				GameObject newActionButtonGO = Instantiate(actionButtonPrefab);
				newActionButtonGO.transform.SetParent(worldCanvas.transform, false);
				newActionButtonGO.transform.position = curChar.transform.position + new Vector3(0f, -100f, 0f);
				Button newActionButton = newActionButtonGO.GetComponent<Button>();
				newActionButton.onClick.AddListener(() => BattleController.instance.AddAction(curChar.characterAction));
				TextMeshProUGUI newActionButtonText = newActionButtonGO.GetComponentInChildren<TextMeshProUGUI>();
				newActionButtonText.text = curChar.characterAction.actionName;
			}
		}
	}
}
