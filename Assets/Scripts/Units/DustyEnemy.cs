using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DustyEnemy : Unit
{
    void Start()
    {
        unitName = "Dusty";
        maxHP = 25;
        attackStat = 7;
        defenseStat = 5;
        speedStat = 3;
        currentHP = maxHP;
        IsPlayerControlled = false;
        behavior =  this.gameObject.AddComponent<DustyBehavior>();
    }


    public class DustyBehavior : EnemyBehavior
    {
        private int turn;
        private BattleSystem battlesystem; 
        public override void DoBehavior(Unit baseUnit)
        {
            battlesystem = GameObject.FindObjectOfType<BattleSystem>();
            var num = UnityEngine.Random.Range(0, battlesystem.numOfUnits.Count);
            print(num);
            if (battlesystem.numOfUnits[num].IsPlayerControlled)
            {
                switch (baseUnit.actionList[turn].targetType)
                {
                    case Action.TargetType.ANY:
                        baseUnit.actionList[turn].target = battlesystem.numOfUnits[num];
                        break;
                    case Action.TargetType.SELF:
                        baseUnit.actionList[turn].target = baseUnit;
                        break;
                }
                baseUnit.actionList[turn].unit = baseUnit;
                if (baseUnit.actionList[turn].speed == 0)
                { baseUnit.actionList[turn].speed = baseUnit.speedStat; }
                battlesystem.AddAction(baseUnit.actionList[turn]);
                baseUnit.state = PlayerState.READY;
                BattleSystem.AddReadyUnit(baseUnit);
                battlesystem.DisplayEnemyIntent(baseUnit.actionList[turn], baseUnit);
                if(turn != 2) { turn += 1; }
                else { 
                    turn = 0;
                    StartCoroutine(ForceBattleEnd());
                }
            }
            else
            {
                try
                {
                    DoBehavior(baseUnit);
                }
                catch { }
            }
        }

        private IEnumerator ForceBattleEnd()
        {
            yield return new WaitForSeconds(1f);
            SceneManager.sceneLoaded += AddDusty;
            StartCoroutine(Tools.SmoothMove(Tools.GetDirector().BL.gameObject, 0.001f, 60, 0, -3.6f));
            battlesystem.TransitionToMap();
        }
        private void AddDusty(Scene scene, LoadSceneMode mode)
        {
            var d = GameObject.FindObjectOfType<Director>();
            Director.AddUnitToParty("Dusty");
            SceneManager.sceneLoaded -= AddDusty;

        }
    }
}
