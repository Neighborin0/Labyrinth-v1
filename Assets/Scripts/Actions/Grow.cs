using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[CreateAssetMenu(fileName = "Grow", menuName = "Assets/Actions/Grow")]
public class Grow : Action
{
    private void OnEnable()
    {
        ActionName = "Grow";
        damage = 2;
        accuracy = 1;
        targetType = TargetType.SELF;
        actionType = ActionType.STATUS;
        description = "The user undergoes a sudden growth. Raises ATK by 2.";
    }
    public override void OnActivated()
    {
        BattleSystem.SetStatChanges(Stat.ATK, 2, false, target);
        Debug.Log(unit.unitName + "'s attack went up!");
    }
}
