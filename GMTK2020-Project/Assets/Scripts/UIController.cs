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
		public static UIController instance;

		public List<Button> actionButtons;
		public Canvas worldCanvas;
		public GameObject actionButtonPrefab;
		public GameObject healthBarPrefab;
		public GameObject controlBarPrefab;
		public Vector3 actionButtonOffset;
		public Vector3 healthBarOffset;
		public Vector3 controlBarOffset;


		private void Awake()
		{
			actionButtons = new List<Button>();
			instance = this;
		}

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

				case BattleState.SetupPlayerTurn:
					EnableActionButtons();
					break;

				case BattleState.PlayerActionSelect:
					ShowHideBattleActionButtons(true);
					break;

				case BattleState.PlayerActionTargetSelect:
					ShowHideBattleActionButtons(false);
					break;

				case BattleState.PrePlayerActionPlayback:
					ShowHideBattleActionButtons(false);
					break;

				default:
					break;
			}
		}

		private void EnableActionButtons()
		{
			foreach(Button curButton in actionButtons)
			{
				curButton.interactable = true;
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
				GameObject newHealthBarGO = Instantiate(healthBarPrefab);
				newHealthBarGO.transform.SetParent(worldCanvas.transform, false);
				newHealthBarGO.transform.position = curChar.transform.position + healthBarOffset;
				curChar.healthBarGO = newHealthBarGO;
				Slider newHealthBarSlider = newHealthBarGO.GetComponent<Slider>();
				newHealthBarSlider.maxValue = curChar.charStats["Health"].max;
				newHealthBarSlider.minValue = 0;
				newHealthBarSlider.value = curChar.charStats["Health"].cur;

				if(curChar is NeutralUnit) { continue; }

				GameObject newControlBarGO = Instantiate(controlBarPrefab);
				newControlBarGO.transform.SetParent(worldCanvas.transform, false);
				newControlBarGO.transform.position = curChar.transform.position + controlBarOffset;
				curChar.controlBarGO = newControlBarGO;
				Slider newControlBarSlider = newControlBarGO.GetComponent<Slider>();
				newControlBarSlider.maxValue = curChar.charStats["Control"].max;
				newControlBarSlider.minValue = 0;
				newControlBarSlider.value = curChar.charStats["Control"].cur;
			}
		}

		public void SpawnBattleActionButtons()
		{
			foreach (Character curChar in BattleController.instance.battleCharacters)
			{
				if (!curChar.isControllable) { continue; }
				GameObject newActionButtonGO = Instantiate(actionButtonPrefab);
				newActionButtonGO.transform.SetParent(worldCanvas.transform, false);
				newActionButtonGO.transform.position = curChar.transform.position + actionButtonOffset;
				curChar.actionButtonGO = newActionButtonGO;
				Button newActionButton = newActionButtonGO.GetComponent<Button>();
				newActionButton.onClick.AddListener(() => BattleController.instance.PlayerSelectedAction(curChar));
				actionButtons.Add(newActionButton);
				TextMeshProUGUI newActionButtonText = newActionButtonGO.GetComponentInChildren<TextMeshProUGUI>();
				newActionButtonText.text = curChar.characterAction.actionName;
				newActionButtonGO.SetActive(false);
			}
		}

		public void DisableActionButton(GameObject buttonToDisable)
		{
			buttonToDisable.GetComponent<Button>().interactable = false;
		}

		public void ShowHideBattleActionButtons(bool shouldShow)
		{
			foreach(Button curButton in actionButtons)
			{
				curButton.gameObject.SetActive(shouldShow);
			}
		}

		public void OnButtonEndTurn()
		{
			BattleStateMachine.instance.ChangeState(BattleState.PrePlayerActionPlayback);
		}

		public void OnButtonCancelAction()
		{
			BattleController.instance.PlayerCanceledAction();
		}

		public void OnButtonCancelAllActions()
		{

		}

		public void UpdateHealthUI(Character curCharacter)
		{
			Slider curHealthBarSlider = curCharacter.healthBarGO.GetComponent<Slider>();
			curHealthBarSlider.value = curCharacter.charStats["Health"].cur;
		}

		public void UpdateControlUI(Character curCharacter)
		{
			Slider curControlBarSlider = curCharacter.controlBarGO.GetComponent<Slider>();
			curControlBarSlider.value = curCharacter.charStats["Control"].cur;
		}
	}
}
