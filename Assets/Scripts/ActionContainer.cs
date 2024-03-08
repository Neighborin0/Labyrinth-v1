using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ActionContainer : MonoBehaviour
{
    public Action action;
    public TextMeshProUGUI textMesh;
    public bool targetting = false;
    private Unit baseUnit;
    private Button button;
    public TextMeshProUGUI damageNums;
    public TextMeshProUGUI accuracyNums;


    
    void Start()
    {
        baseUnit = this.gameObject.GetComponentInParent<Unit>();
        button = gameObject.GetComponent<Button>();
        button.interactable = true;
        Unit[] units = UnityEngine.Object.FindObjectsOfType<Unit>();
        foreach (var unit in units)
        {
          unit.IsHighlighted = false;
        }
    }
  
    void Update()
    {
        
        if (targetting)
        {
            
            switch (action.targetType)
            {
                case Action.TargetType.ANY:
                    var hit = Tools.GetMousePos();
                    foreach (var unit in Tools.GetAllUnits())
                    {
                        if(unit != baseUnit)
                        {
                            unit.IsHighlighted = true;
                        }
                    }
                    if (Input.GetMouseButtonUp(0))
                    {
                        try
                        {
                           
                            if (hit.collider.gameObject != null && hit.collider != baseUnit.gameObject.GetComponent<Collider2D>() && hit.collider.gameObject.GetComponent<Unit>() != null)
                            {
                                var unit = hit.collider.GetComponent<Unit>();
                                foreach (var z in Tools.GetAllUnits())
                                {
                                    z.IsHighlighted = false;
                                }
                                action.target = unit;
                                action.unit = baseUnit;
                                baseUnit.state = PlayerState.READY;
                                BattleSystem.AddReadyUnit(baseUnit);
                                BattleSystem.SetUIOff(baseUnit);
                                var battlesystem = GameObject.FindObjectOfType<BattleSystem>();
                                battlesystem.AddAction(action);
                                this.targetting = false;                                
                                SetActive();

                            }
                        }
                        catch (Exception e) { }
                    }
                    break;
                 case Action.TargetType.SELF:
                    try
                    {
                        baseUnit.IsHighlighted = true;
                        if (Input.GetMouseButtonUp(0))
                        {
                            var bU = Tools.GetMousePos();
                            if (bU.collider.gameObject != null && bU.collider == baseUnit.gameObject.GetComponent<Collider2D>())
                            {
                                action.target = baseUnit;
                                action.unit = baseUnit;
                                action.speed = action.unit.speedStat;
                                baseUnit.state = PlayerState.READY;
                                BattleSystem.AddReadyUnit(baseUnit);
                                var battlesystem1 = GameObject.FindObjectOfType<BattleSystem>();
                                battlesystem1.AddAction(action);
                                SetActive();
                                BattleSystem.SetUIOff(baseUnit);
                                baseUnit.IsHighlighted = false;
                            }
                        }
                    }
                    catch (Exception e) { }
                    break;
            }
        }
        
    }

    public void SetActive()
    {
        if (targetting == true)
        {
            targetting = false;
            BattleLog.SetBattleText("");
            //button.interactable = true;
            print("not targetting");
        }
        else
        {
            ActionContainer[] actionContainers = UnityEngine.Object.FindObjectsOfType<ActionContainer>();
            foreach (var x in actionContainers)
            {
                if (x != this)
                {
                    var button = x.GetComponent<Button>();
                    button.interactable = true;
                    x.targetting = false;
                    foreach (var z in Tools.GetAllUnits())
                    {
                      z.IsHighlighted = false;
                    }


                }
            }
            targetting = true;
            print("targetting");
            BattleLog.SetBattleText(action.description);
            button.interactable = false;
        }

 
    }
   
}
