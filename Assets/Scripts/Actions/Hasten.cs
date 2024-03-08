using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Hasten", menuName = "Assets/Actions/Hasten")]
public class Hasten : Action
{
    private void OnEnable()
    {
        ActionName = "Hasten";
        damage = 2;
        accuracy = 1;
        targetType = TargetType.SELF;
        actionType = ActionType.STATUS;
        description = "The user lightens their body, allowing the user to move faster. Increases SPD by 5";
    }
    public override void OnActivated()
    {
        BattleSystem.SetStatChanges(Stat.SPD, 5, false, target);
        //Debug.Log("speed rose");

    }
}
