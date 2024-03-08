using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Bite", menuName = "Assets/Actions/Bite")]
public class Bite : Action
{
    private void OnEnable()
    {
        ActionName = "Bite";
        damage = 5;
        accuracy = 1;
        actionType = ActionType.ATTACK;
        targetType = TargetType.ANY;
        description = "The user viciously gnashes into their target's flesh, dealing " + damage.ToString() + " damage.";


    }
    public override void OnActivated()
    {
        target.health.TakeDamage(damage + unit.attackStat);
        Debug.Log("enemy is attacking");
    }
}
