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

        private Queue<Action> battleActions;
		private Action curAction;

		private Camera mainCamera;
		
		private void Awake()
		{
			instance = this;
			battleActions = new Queue<Action>();
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
					BattleStateMachine.instance.ChangeState(BattleState.PlayerActionSelect);
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
						}						
					}
					break;

				case BattleState.PlayerActionPlayback:
					ExecuteActions();
					break;

				case BattleState.EnemyActionSelect:
					BattleStateMachine.instance.ChangeState(BattleState.EnemyActionPlayback);
					break;

				case BattleState.EnemyActionPlayback:
					ExecuteActions();
					break;

				default:
					break;
			}
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

		public void AddAction(Action newAction)
		{
			battleActions.Enqueue(newAction);
		}

		public void ClearActions()
		{
			battleActions.Clear();
		}

		public void ExecuteActions()
		{
			Debug.Log("Executing actions");
            while(battleActions.Count > 0)
			{
                Action curAction = battleActions.Dequeue();
                curAction.Execute();
			}

			switch (BattleStateMachine.instance.curBattleState)
			{
				case BattleState.PlayerActionPlayback:
					BattleStateMachine.instance.ChangeState(BattleState.EnemyActionSelect);
					break;

				case BattleState.EnemyActionPlayback:
					BattleStateMachine.instance.ChangeState(BattleState.PlayerActionSelect);
					break;
			}
		}
    }
}