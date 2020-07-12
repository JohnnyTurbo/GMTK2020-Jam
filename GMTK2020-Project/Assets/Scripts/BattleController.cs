using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TMG.GMTK2020 
{
    public class BattleController : MonoBehaviour
    {
        public static BattleController instance;

        public List<Character> battleCharacters;

        private List<Action> battleActions;
		private Action curAction;

		private Camera mainCamera;
		private bool shouldContinue;

		private void Awake()
		{
			instance = this;
			battleActions = new List<Action>();
			mainCamera = Camera.main;
		}

		private void Update()
		{
			switch (BattleStateMachine.instance.curBattleState)
			{
				case BattleState.PreNewBattleSetup:
					BattleStateMachine.instance.ChangeState(BattleState.NewBattleSetup);
					break;

				case BattleState.NewBattleSetup:
					BattleStateMachine.instance.ChangeState(BattleState.SetupPlayerTurn);
					break;

				case BattleState.SetupPlayerTurn:
					SetupPlayerTurn();
					break;

				case BattleState.PlayerActionTargetSelect:
					if (Input.GetMouseButtonDown(0))
					{
						RaycastHit hit;
						Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

						if(Physics.Raycast(ray, out hit))
						{
							Character targetChar = hit.transform.GetComponent<Character>();
							if(targetChar == null) { break; }
							curAction.target = targetChar;
							AddAction(curAction);
							BattleStateMachine.instance.ChangeState(BattleState.PlayerActionSelect);
							UIController.instance.DisableActionButton(curAction.source.actionButtonGO);
						}						
					}
					break;

				case BattleState.PrePlayerActionPlayback:
					StartCoroutine(ExecuteActions());
					BattleStateMachine.instance.ChangeState(BattleState.PlayerActionPlayback);
					break;

				case BattleState.PlayerActionPlayback:
					break;

				case BattleState.PostPlayerActionPlayback:
					//CheckForDeadUnits();
					BattleStateMachine.instance.ChangeState(BattleState.EnemyActionSelect);
					break;

				case BattleState.EnemyActionSelect:
					BattleStateMachine.instance.ChangeState(BattleState.PreEnemyActionPlayback);
					break;

				case BattleState.PreEnemyActionPlayback:
					StartCoroutine(ExecuteActions());
					BattleStateMachine.instance.ChangeState(BattleState.EnemyActionPlayback);
					break;

				case BattleState.EnemyActionPlayback:
					break;

				default:
					break;
			}
		}

		private void SetupPlayerTurn()
		{
			DialogueController.instance?.OneLiner("Narrator", "Player Turn Begins!", BeginPlayerTurn);
		}

		public void BeginPlayerTurn()
		{
			BattleStateMachine.instance.ChangeState(BattleState.PlayerActionSelect);
		}

		public void BeginBattle()
		{
			BattleStateMachine.instance.ChangeState(BattleState.PreNewBattleSetup);
		}

		public void RegisterCharacter(Character newCharacter)
		{
			battleCharacters.Add(newCharacter);
		}

		public void RemoveCharacter(Character characterToRemove)
		{
			battleCharacters.Remove(characterToRemove);
		}

		public void PlayerSelectedAction(Action newAction)
		{			
			curAction = newAction;
			BattleStateMachine.instance.ChangeState(BattleState.PlayerActionTargetSelect);
		}

		public void AddActionToFront(Action newAction)
		{
			battleActions.Insert(0, newAction);
		}

		public void AddAction(Action newAction)
		{
			battleActions.Add(newAction);
		}

		public void ClearActions()
		{
			battleActions.Clear();
		}

		public IEnumerator ExecuteActions()
		{
			Debug.Log("Executing actions");
            while(battleActions.Count > 0)
			{
				shouldContinue = false;
				Action curAction = battleActions[0];
				battleActions.RemoveAt(0);
				DialogueController.instance.OneLiner("Narrator", curAction.Execute(), ContinueDialogue);
				yield return new WaitUntil(() => shouldContinue == true);
			}

			switch (BattleStateMachine.instance.curBattleState)
			{
				case BattleState.PlayerActionPlayback:
					BattleStateMachine.instance.ChangeState(BattleState.PostPlayerActionPlayback);
					break;

				case BattleState.EnemyActionPlayback:
					BattleStateMachine.instance.ChangeState(BattleState.SetupPlayerTurn);
					break;
			}
		}

		private void ContinueDialogue()
		{
			shouldContinue = true;
		}
    }
}