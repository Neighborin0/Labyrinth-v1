using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : Unit
{
    void Start()
    {
        unitName = "Vermin";
        maxHP = UnityEngine.Random.Range(20, 25);
        attackStat = UnityEngine.Random.Range(4, 7);
        defenseStat = UnityEngine.Random.Range(2 , 5);
        speedStat = UnityEngine.Random.Range(2, 7);
        currentHP = maxHP;
        IsPlayerControlled = false;
        behavior = new RandomEnemyBehavior();
    }
}
