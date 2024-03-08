using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dusty : Unit
{

    private List<string> intro = new List<string>
   {
        "yo.",
   };

    public override void Intro()
    {
        introText = intro;
    }
    void Start()
    {
        unitName = "Dusty";
        maxHP = 25;
        attackStat = 7;
        defenseStat = 5;
        speedStat = 3;
        currentHP = maxHP;
        IsPlayerControlled = true;
    }

  


}
