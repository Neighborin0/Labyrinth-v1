using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class You : Unit
{
    void Start()
    {
        unitName = "Aurelia";
        maxHP = 60;
        attackStat = 5;
        defenseStat = 5;
        speedStat = 5;
        currentHP = maxHP;
        IsPlayerControlled = true;
    }
}
