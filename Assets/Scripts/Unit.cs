using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public enum PlayerState { IDLE, DECIDING, READY, BATTLING }
public enum Stat { ATK, DEF, SPD }
public class Unit : MonoBehaviour
{
    public string unitName;
    public int currentHP;
    public bool IsPlayerControlled;
    public List<GameObject> skillUIs;
    public IntentContainer intentUI;
    public Healthbar health;
    public bool IsHighlighted;
    public bool IsDead;
    public EnemyBehavior behavior;

    //text stuff
    public List<string> introText;
    public List<Item> inventory;

    public event Action<Unit> BattlePhaseEnd;


    //player states
    public PlayerState state;

    //stats
    public int maxHP;
    public int attackStat;
    public int defenseStat;
    public int speedStat;

    //actions
    public List<Action> actionList;
     void Start()
    {
        currentHP = maxHP;
        Debug.Log(maxHP);
        Debug.Log(attackStat);
        Debug.Log(defenseStat);
        Debug.Log(speedStat);
        state = PlayerState.IDLE;
        var particleSys = this.GetComponent<ParticleSystem>();
        particleSys.Stop();

    }


    void Update()
    {
        if(IsHighlighted)
        {
            var sprite = this.gameObject.GetComponent<SpriteRenderer>();
            sprite.material.SetColor("_OutlineColor", Color.white);
        }
        else
        {
            var sprite = this.gameObject.GetComponent<SpriteRenderer>();
            sprite.material.SetColor("_OutlineColor", Color.black);
        }

        if (Input.GetMouseButtonUp(0))
        {
            var hit = Tools.GetMousePos();
            if (hit.collider != null && hit.collider == this.GetComponent<BoxCollider2D>())
            {
                BattleLog.DisplayCharacterStats(this);
            }
        }

      /*  if(state == PlayerState.READY)
        {
            if (Input.GetMouseButtonUp(0))
            {
                var hit = Tools.GetMousePos();
                if (hit.collider != null && hit.collider == this.GetComponent<BoxCollider2D>() && this.IsPlayerControlled)
                {
                    StartDecision();
                    var bs = GameObject.FindObjectOfType<BattleSystem>();
                    bs.numOfReadyUnits.Remove(this);
                    foreach(var action in bs.ActionsToPerform.ToList())
                    {
                        if(action.unit = this)
                        {
                            bs.ActionsToPerform.Remove(action);   
                            break;
                        }
                    }
                }
            }
        }
      */
            if (state == PlayerState.IDLE)
        {
            if (Input.GetMouseButtonUp(0))
            {
                var hit = Tools.GetMousePos();
                if (hit.collider != null && hit.collider == this.GetComponent<BoxCollider2D>() && this.IsPlayerControlled)
                {
                    StartDecision();
                }
            }
        }
        if (state == PlayerState.DECIDING)
        {
            if (Input.GetMouseButtonUp(1))
            {
                ExitDecision();

            }
        }
    }
    public virtual void Intro()
    {

    }
    public void DoBattlePhaseEnd()
    {
        BattlePhaseEnd?.Invoke(this);
    }
    public void StartDecision()
    {
        BattleSystem.SetUIOn(this);
        print("Unit is deciding an action");
    }

    public void ExitDecision()
    {
        BattleLog.DisableCharacterStats();
        BattleLog.SetRandomAmbientTextActive();
        BattleLog.ClearBattleText();
        foreach (var z in Tools.GetAllUnits())
        {
            z.IsHighlighted = false;
        }
        BattleSystem.SetUIOff(this);
        state = PlayerState.IDLE;
        print("no thinky");
    }


    

}
