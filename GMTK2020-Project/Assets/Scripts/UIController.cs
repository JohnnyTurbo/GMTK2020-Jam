using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace TMG.GMTK2020
{
	public class UIController : MonoBehaviour
	{
		public Canvas worldCanvas;
		public GameObject actionButtonPrefab;
		public GameObject healthBarPrefab;
		public GameObject controlBarPrefab;
		public Vector3 actionButtonOffset;
		public Vector3 healthBarOffset;
		public Vector3 controlBarOffset;

		private List<Button> actionButtons;
		private List<Slider> healthBars;

		private void Start()
		{
			BattleStateMachine.instance.stateChanged += Instance_stateChanged;
		}

		private void Instance_stateChanged(object sender, BattleState newState)
		{
			switch (newState)
			{
				case BattleState.NewBattleSetup:
					InitializeBattleUI();
					break;

				case BattleState.PlayerActionSelect:
					break;

				default:
					break;
			}
		}

		private void InitializeBattleUI()
		{
			DisplayCharacterStats();
			SpawnBattleActionButtons();
		}

		private void DisplayCharacterStats()
		{
			foreach(Character curChar in BattleController.instance.battleCharacters)
			{
				//GameObject newHealthBar = Instantiate()
			}
		}

		public void SpawnBattleActionButtons()
		{
			foreach (Character curChar in BattleController.instance.battleCharacters)
			{
				if (!curChar.isControllable) { continue; }
				GameObject newActionButtonGO = Instantiate(actionButtonPrefab);
				newActionButtonGO.transform.SetParent(worldCanvas.transform, false);
				newActionButtonGO.transform.position = curChar.transform.position + new Vector3(0f, -100f, 0f);
				Button newActionButton = newActionButtonGO.GetComponent<Button>();
				newActionButton.onClick.AddListener(() => BattleController.instance.AddAction(curChar.characterAction));
				actionButtons.Add(newActionButton);
				TextMeshProUGUI newActionButtonText = newActionButtonGO.GetComponentInChildren<TextMeshProUGUI>();
				newActionButtonText.text = curChar.characterAction.actionName;
			}
		}

		public void ShowHideBattleActionButtons(bool shouldShow)
		{
			foreach(Button curButton in actionButtons)
			{
				curButton.gameObject.SetActive(shouldShow);
			}
		}
	}
}
