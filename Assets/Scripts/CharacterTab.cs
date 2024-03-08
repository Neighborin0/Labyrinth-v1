using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

public class CharacterTab : MonoBehaviour
{
    public List<Button> buttons;
    public Unit unit;
    [SerializeField] public TextMeshProUGUI statDisplay;
     void Update()
    {
        if (unit != null)
        statDisplay.text = ("HP:" + unit.maxHP.ToString() + "\n" +  "ATK:" + unit.attackStat.ToString() + "\n" + "DEF:" + unit.defenseStat.ToString() + "\n" + "SPD:" + unit.speedStat.ToString());
    }
    public void IncreaseStat()
    {
        Director.LabyrinthLVL += 1;
        if(EventSystem.current.currentSelectedGameObject == buttons[0].gameObject)
        {
            unit.maxHP += 3;
        }
        else if (EventSystem.current.currentSelectedGameObject == buttons[1].gameObject)
        {
            unit.attackStat += 1;
        }
        else if (EventSystem.current.currentSelectedGameObject == buttons[2].gameObject)
        {
            unit.defenseStat += 1;
        }
        else if (EventSystem.current.currentSelectedGameObject == buttons[3].gameObject)
        {
            unit.speedStat += 1;
        }
        foreach(var x in buttons)
        {
            x.interactable = false;
        }
        Destroy(this.gameObject);
        if (GameObject.FindObjectsOfType<CharacterTab>().Length <= 1)
        {
            SceneManager.LoadScene(0);
        }
    }
  
}
