using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Calm", menuName = "Assets/Actions/Calm")]
public class Calm : Action
{
    private void OnEnable()
    {
        ActionName = "Calm";
        damage = 2;
        accuracy = 1;
        actionType = ActionType.STATUS;
        targetType = TargetType.SELF;
        description = "The user calms themselves, giving them a clear mind to properly assess the battlefield. Increases DEF by 2.";
    }
    public override void OnActivated()
    {
        BattleSystem.SetStatChanges(Stat.DEF, 2, false, target);
    }
}
