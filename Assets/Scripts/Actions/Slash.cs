using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Slash", menuName = "Assets/Actions/Slash")]
public class Slash : Action
{
    private void OnEnable()
    {
        ActionName = "Slash";
        damage = 5;
        accuracy = 1;
        targetType = TargetType.ANY;
        actionType = ActionType.ATTACK;
        description = "The target is attacked with a powerful slash. Deals " + damage + " damage.";
    }
    public override void OnActivated()
    {
        //BattleLog.SetBattleText(unit.unitName + " " + "attacked for" + " " + (damage + unit.attackStat - target.defenseStat) + " " + " damage!");
        target.health.TakeDamage(damage + unit.attackStat);     
    }
}
