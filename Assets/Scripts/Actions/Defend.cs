using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Defend", menuName = "Assets/Actions/Defend")]
public class Defend : Action
{
    private int playerDef;
    private void OnEnable()
    {
        ActionName = "Defend";
        accuracy = 1;
        actionType = ActionType.STATUS;
        PriorityMove = true;
        targetType = TargetType.SELF;
        description = "The user enters a defensive stance. Temporarily doubles DEF.";
    }

    public override void OnActivated()
    {
        playerDef = (int)Math.Ceiling(unit.defenseStat * 2f);
        BattleSystem.SetStatChanges(Stat.DEF, playerDef, false, target);
        unit.BattlePhaseEnd += RemoveStatChanges;
        Debug.Log("enemy is attacking");
    }

    private void RemoveStatChanges(Unit unit)
    {
        BattleSystem.SetStatChanges(Stat.DEF, -playerDef, false, unit);
        unit.BattlePhaseEnd -= RemoveStatChanges;
    }
}
