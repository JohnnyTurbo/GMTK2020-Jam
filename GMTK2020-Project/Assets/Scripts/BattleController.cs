using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMG.GMTK2020 
{
    public class BattleController : MonoBehaviour
    {
        public static BattleController instance;

        public Queue<Action> battleActions;
        public List<Character> battleCharacters;

		private void Awake()
		{
			instance = this;
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