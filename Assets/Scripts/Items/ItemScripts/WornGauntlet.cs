using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
[CreateAssetMenu(fileName = "WornGauntlet", menuName = "Assets/Items/WornGauntlet")]
public class WornGauntlet : Item
{
    public override void OnPickup(Unit unit)
    {
        Debug.Log("unit attack should be raised");
        unit.attackStat += 2;
    }
}
