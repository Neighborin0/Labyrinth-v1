using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Action : ScriptableObject
{

    //general action parameters
    public string ActionName;
    public int damage;
    public int accuracy;
    public string description;
    public int speed;
    public bool PriorityMove;
    public enum TargetType { ANY, SELF};
    public enum ActionType { ATTACK, STATUS };

    public TextMeshProUGUI text;
   // public GameObject targettingbutton;
    public Unit unit;
    public TargetType targetType;
    public ActionType actionType;
    public Unit target;

    void Start()
    {
        text.text = ActionName;
        //Init();
    }


   // public abstract void Init();

    public virtual void OnActivated(){ }
}
