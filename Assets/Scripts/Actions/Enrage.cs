using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Enrage", menuName = "Assets/Actions/Enrage")]
public class Enrage : Action
{
    private void OnEnable()
    {
        ActionName = "Enrage";
        damage = 2;
        accuracy = 1;
        actionType = ActionType.STATUS;
        targetType = TargetType.SELF;
        description = "The user enters into a burning rage. Raises ATK by 2.";
    }
    public override void OnActivated()
    {
        BattleSystem.SetStatChanges(Stat.ATK, 2, false, target);
    }
}
