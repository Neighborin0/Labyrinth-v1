using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Slow", menuName = "Assets/Actions/Slow")]
public class Slow : Action
{
    private void OnEnable()
    {
        ActionName = "Slow";
        damage = 2;
        accuracy = 1;
        targetType = TargetType.ANY;
        actionType = ActionType.STATUS;
        description = "The user inflicts the foe with a temporal curse, making thier movements sluggish. Decreases SPD by 2.";
    }
    public override void OnActivated()
    {
        BattleSystem.SetStatChanges(Stat.DEF, -2, false, target);
    }
}
