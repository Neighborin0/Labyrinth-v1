using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatNode : MonoBehaviour
{
    public List<Unit> enemies;
    public List<Unit> playerUnits;
    public void StartBattle()
    {
        var director = GameObject.FindObjectOfType<Director>();
        print(director.party.Count);
        Tools.ClearAllCharacterSlots();
        foreach (var unit in director.party)
        {
            playerUnits.Add(unit);
            print(unit.unitName);
        }
        SceneManager.LoadScene(1);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var battlesystem = GameObject.FindObjectOfType<BattleSystem>();
        battlesystem.playerUnits = playerUnits;
        battlesystem.enemyUnits = enemies;
        print(battlesystem.enemyUnits.Count);
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
