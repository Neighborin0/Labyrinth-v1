using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TutorialEnemy : Unit
{
    void Start()
    {
        unitName = "Husk";
        maxHP = 30;
        attackStat = 2;
        defenseStat = 6;
        speedStat = 3;
        currentHP = maxHP;
        IsPlayerControlled = false;
        behavior =  this.gameObject.AddComponent<TutorialEnemyBehavior>();
    }


    public class TutorialEnemyBehavior : EnemyBehavior
    {
        private int turn;
        private Director d;
        public override void DoBehavior(Unit baseUnit)
        {
            d = GameObject.FindObjectOfType<Director>();
            var battlesystem = GameObject.FindObjectOfType<BattleSystem>();
            var num = UnityEngine.Random.Range(0, battlesystem.numOfUnits.Count);
            print(num);
            print("turn" + turn);
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
                switch(turn)
                {
                  
                    case 1:
                        if (!battlesystem.numOfUnits[num].actionList.Contains(d.actionDatabase.Where(obj => obj.name == "Enrage").SingleOrDefault()))
                        battlesystem.numOfUnits[num].actionList.Add(d.actionDatabase.Where(obj => obj.name == "Enrage").SingleOrDefault());
                        break;
                    case 2:
                        if (!battlesystem.numOfUnits[num].actionList.Contains(d.actionDatabase.Where(obj => obj.name == "Defend").SingleOrDefault()))
                        battlesystem.numOfUnits[num].actionList.Add(d.actionDatabase.Where(obj => obj.name == "Defend").SingleOrDefault());
                        break;  
                }
                if(turn != 2) { turn += 1; }
                else { turn = 0; }
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
    }
}
