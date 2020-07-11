using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMG.GMTK2020;

namespace Tests
{
    public class StateMachineTests
    {
        [UnityTest]
        public IEnumerator Test_States_Entering_Battle()
        {
            GameObject battleStateMachineGO = new GameObject();
            BattleStateMachine battleStateMachine = battleStateMachineGO.AddComponent<BattleStateMachine>();

            GameObject battleControllerGO = new GameObject();
            BattleController battleController = battleControllerGO.AddComponent<BattleController>();

            Assert.AreEqual(BattleState.None, BattleStateMachine.instance.curBattleState);

            battleController.BeginBattle();

            Assert.AreEqual(BattleState.PreNewBattleSetup, BattleStateMachine.instance.curBattleState);

            yield return null;

            Assert.AreEqual(BattleState.NewBattleSetup, BattleStateMachine.instance.curBattleState);

            yield return null;

            Assert.AreEqual(BattleState.PlayerActionSelect, BattleStateMachine.instance.curBattleState);
        }
    }
}
