using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class RandomEnemyBehavior : EnemyBehavior
{
    public override void DoBehavior(Unit baseUnit)
    {
        var battlesystem = GameObject.FindObjectOfType<BattleSystem>();
        var num = UnityEngine.Random.Range(0, battlesystem.numOfUnits.Count);
        print(num);
        if (battlesystem.numOfUnits[num].IsPlayerControlled)
        {
            int move = UnityEngine.Random.Range(0, baseUnit.actionList.Count);
            switch (baseUnit.actionList[move].targetType)
            {
                case Action.TargetType.ANY:
                    baseUnit.actionList[move].target = battlesystem.numOfUnits[num];
                    break;
                case Action.TargetType.SELF:
                    baseUnit.actionList[move].target = baseUnit;
                    break;
            }
            baseUnit.actionList[move].unit = baseUnit;
            if (baseUnit.actionList[move].speed == 0)
            { baseUnit.actionList[move].speed = baseUnit.speedStat; }
            battlesystem.AddAction(baseUnit.actionList[move]);
            baseUnit.state = PlayerState.READY;
            BattleSystem.AddReadyUnit(baseUnit);
            battlesystem.DisplayEnemyIntent(baseUnit.actionList[move], baseUnit);
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

