using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class CharacterJoinSceneScript : MonoBehaviour
{
    private Unit chosenUnit;
    void Start()
    {
        var director = GameObject.FindObjectOfType<Director>();
        chosenUnit = director.characterdatabase[UnityEngine.Random.Range(0, director.characterdatabase.Count)];
        chosenUnit.Intro();
        //print(unit.introText[0]);
       StartCoroutine(Tools.SmoothMove(director.BL.gameObject, 0f, 60, 0, 3.4f));
        StartIntro(chosenUnit);

    }

    private void StartIntro(Unit unit)
    {
        BattleLog.CharacterDialog(unit.introText);
    }

    public void AddToParty()
    {
        var director = GameObject.FindObjectOfType<Director>();
        StartCoroutine(Tools.SmoothMove(director.BL.gameObject, 0f, 60, 0, -3.4f));
        Director.AddUnitToParty(chosenUnit.name);
        SceneManager.LoadScene(0);
    }



}
