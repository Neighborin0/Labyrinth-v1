using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BattleLog : MonoBehaviour
{
    public TextMeshProUGUI ambientText;
    public TextMeshProUGUI battleText;

    int index;
    //character stat text
    public TextMeshProUGUI STATtext;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI enemyIntent;

    //character dialog
    public TextMeshProUGUI characterdialog;




    private static string[] ambience = new string[]
        {
            "You hear the skittering of vermin...",
            "A foul stench permeates the air.",
            "You feel an uneasy presence around you.",
        };

    
    public static void ClearBattleLog()
    {
        var battlelog = GameObject.FindObjectOfType<BattleLog>();
        battlelog.ambientText.text = "";
    }

    public static void CharacterDialog(List<string> dialog)
    {
        var battlelog = GameObject.FindObjectOfType<BattleLog>();
        battlelog.characterdialog.gameObject.SetActive(true);
        battlelog.StartCoroutine(battlelog.TypeMultiText(dialog, 0.01f, battlelog.characterdialog, true));
    }


    public static void SetRandomAmbientTextActive()
    {
        var battlelog = GameObject.FindObjectOfType<BattleLog>();
        BattleLog.ClearAllBattleLogText();
        battlelog.ambientText.gameObject.SetActive(true);
    }

    public static void SetRandomAmbientTextOff()
    {
        var battlelog = GameObject.FindObjectOfType<BattleLog>();
        battlelog.ambientText.gameObject.SetActive(false);
    }
    public void CreateRandomAmbientText()
    {
        var text = ambience[UnityEngine.Random.Range(0, ambience.Length)];
        ambientText.text = "";
        StartCoroutine(TypeText(text, 0.03f, ambientText, false));
    }

    public static void DisplayCharacterStats(Unit unit)
    {
        var battlelog = GameObject.FindObjectOfType<BattleLog>();
        BattleLog.ClearAllBattleLogText();
        BattleLog.ClearBattleText();
        battlelog.STATtext.gameObject.SetActive(true);
        battlelog.characterName.gameObject.SetActive(true);
        battlelog.STATtext.text = ("ATK:" + unit.attackStat.ToString() + "\n" + "DEF:" + unit.defenseStat.ToString() + "\n" + "SPD:" + unit.speedStat.ToString());
        battlelog.characterName.text = (unit.unitName);
    }

    public static void DisableCharacterStats()
    {
        var battlelog = GameObject.FindObjectOfType<BattleLog>();
        battlelog.STATtext.gameObject.SetActive(false);
        battlelog.characterName.gameObject.SetActive(false);
    }

    public static void DisplayEnemyIntentInfo(string target, string description)
    {
        var battlelog = GameObject.FindObjectOfType<BattleLog>();
        BattleLog.ClearAllBattleLogText();
        battlelog.enemyIntent.gameObject.SetActive(true);
        battlelog.enemyIntent.text = ("Target: " + target + "\n" + description);
    }
   

    public static void SetBattleText(string text)
    {
        var battlelog = GameObject.FindObjectOfType<BattleLog>();
        battlelog.DoBattleText(text);
    } 

    public void DoBattleText(string text)
    {
        var battlelog = GameObject.FindObjectOfType<BattleLog>();
        BattleLog.ClearAllBattleLogText();
        battlelog.ambientText.gameObject.SetActive(false);
        battleText.gameObject.SetActive(true);
        BattleLog.DisableCharacterStats();
        battleText.text = text;
    }

    public static void ClearBattleText()
    {
        var battlelog = GameObject.FindObjectOfType<BattleLog>();
        battlelog.battleText.gameObject.SetActive(false);
    }

    public static void ClearAllBattleLogText()
    {
        var battlelog = GameObject.FindObjectOfType<BattleLog>();
        battlelog.battleText.gameObject.SetActive(false);
        BattleLog.DisableCharacterStats();
        battlelog.ambientText.gameObject.SetActive(false);
        battlelog.enemyIntent.gameObject.SetActive(false);
    }


    private IEnumerator TypeMultiText(List<string> text, float textSpeed, TMP_Text x, bool disableAfter)
    {

        for (int i = 0; i < text.Count; i++)
        {
            foreach (char letter in text[i].ToCharArray())
            {
                x.text += letter;
                yield return new WaitForSeconds(textSpeed);
            }
            yield return new WaitForSeconds(1f);
            characterdialog.text = "";
        }
        if (disableAfter)
        {
            x.text = "";
        }
        var characterjoin = GameObject.FindObjectOfType<CharacterJoinSceneScript>();
        characterjoin.AddToParty();

    }
    private IEnumerator TypeText(string text, float textSpeed, TMP_Text x, bool disableAfter)
    {
        foreach (char letter in text)
        {
            x.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
        if(disableAfter)
        {
            x.text = "";
        }

    }

 
}
