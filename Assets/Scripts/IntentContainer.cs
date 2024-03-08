using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class IntentContainer : MonoBehaviour
{
    public Action action;
    public TextMeshProUGUI textMesh;
    public TextMeshProUGUI damageNums;
    public TextMeshProUGUI target;


    public void DisplayIntentInfo()
    {
        BattleLog.DisplayEnemyIntentInfo(action.target.unitName, action.description);
    }

   
}
