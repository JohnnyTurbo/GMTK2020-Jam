using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMG.GMTK2020 
{
    public class BattleController : MonoBehaviour
    {
        public static BattleController instance;

        public List<Character> battleCharacters;

        private Queue<Action> battleActions;
		
		private void Awake()
		{
			instance = this;
			battleActions = new Queue<Action>();
		}

		private void Update()
		{
			switch (BattleStateMachine.instance.curBattleState)
			{
				case BattleState.RegisterCharacters:
					BattleStateMachine.instance.ChangeState(BattleState.PlayerActionSelect);
					break;

				default:
					break;
			}
		}

		public void BeginBattle()
		{
			BattleStateMachine.instance.ChangeState(BattleState.RegisterCharacters);
		}

		public void RegisterCharacter(Character newCharacter)
		{
			battleCharacters.Add(newCharacter);
		}

		public void RemoveCharacter(Character characterToRemove)
		{
			battleCharacters.Remove(characterToRemove);
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
            while(battleActions.Count > 0)
			{
                Action curAction = battleActions.Dequeue();
                curAction.Execute();
			}
		}
    }
}