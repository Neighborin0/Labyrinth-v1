using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using static UnityEngine.ParticleSystem;

public enum BattleStates { START, DECISION_PHASE, BATTLE, WON, DEAD}
public class BattleSystem : MonoBehaviour
{
    //public GameObject playerUnit1;
    //public GameObject enemyUnit1;
    public Transform BattleOrderpos;
    public BattleOrder BattleOrderImage;
    private BattleLog BL;
    public GameObject statPopUp;


    public List<Transform> playerPositions;
    public List<Transform> enemyPositions;

    public Slider hpslider;
    public Canvas canvas;


    public List<Unit> speedlist;
    public List<Action> ActionsToPerform;
    public List<Action> PriorityActions;
    public List<Unit> numOfReadyUnits;
    public List<Unit> numOfUnits;
    public List<Unit> playerUnits;
    public List<Unit> enemyUnits;


    //Unit player;
    //Unit enemy;

    void Start()
    {
        state = BattleStates.START;
        BL = Tools.GetDirector().BL;
        //var particleSystem = this.GetComponent<ParticleSystem>();
       // particleSystem.Play();
        StartBattle();
    }
    void Update()
    {
        if(numOfReadyUnits.Count == numOfUnits.Count && state == BattleStates.DECISION_PHASE)
        {
            StartBattlePhase();
        }


    }

    public BattleStates state;
   
    public static void AddReadyUnit(Unit unit)
    {
        var battlesystem = FindObjectOfType<BattleSystem>();
        battlesystem.numOfReadyUnits.Add(unit);
    }
    void StartBattle()
    {
        for (int i = 0; i <= playerUnits.Count - 1; i++)
        {
            playerUnits[i].transform.position = playerPositions[i].position;
            playerUnits[i].gameObject.SetActive(true);
            playerPositions[i].DetachChildren();
            SetupHUD(playerUnits[i]);
        }
        for (int i = 0; i <= enemyUnits.Count - 1; i++)
        {

            Instantiate(enemyUnits[i], enemyPositions[i]);
            enemyUnits[i].gameObject.SetActive(true); 
            enemyPositions[i].DetachChildren();
            SetupHUD(enemyUnits[i]);
        }
        StartCoroutine(Transition());
        enemyUnits.Clear();
        playerUnits.Clear();
        print(state);
    }

    IEnumerator Transition()
    {
        yield return new WaitForSeconds(0.5f);
        BL.gameObject.SetActive(true);
        StartDecisionPhase();
    }

    void StartDecisionPhase()
    {

        foreach (var unit in Tools.GetAllUnits())
        {
            numOfUnits.Add(unit);
            if (!unit.IsPlayerControlled)
            {
                for (int i = 0; i <= enemyUnits.Count - 1; i++)
                {
                   if(enemyUnits[i].unitName == unit.unitName)
                    {
                        unit.unitName = unit.unitName + " (" + (i + 2) + ")";
                    }
                }
                enemyUnits.Add(unit);
            }
            else
            {
                playerUnits.Add(unit);
                //DontDestroyOnLoad(unit.gameObject);
            }
            unit.DoBattlePhaseEnd();
        }
        if (playerUnits.Count == 0)
        {
            state = BattleStates.DEAD;
            print("you lose");
        }
        else if (enemyUnits.Count == 0)
        {
            TransitionToMap();
            print("You win");
        }
        else
        {
            foreach (var unit in Tools.GetAllUnits())
            {
                if (!unit.IsPlayerControlled)
                {
                    unit.behavior.DoBehavior(unit);
                }
            }
            state = BattleStates.DECISION_PHASE;
            TurnBattleOrderOn();
            StartCoroutine(BattleOrderImage.TransitionIn(0.06f));
            StartCoroutine(Tools.SmoothMove(BL.gameObject, 0.001f, 60, 0, 3.6f));
            BattleLog.SetRandomAmbientTextActive();
            BL.CreateRandomAmbientText();
            print(state);
        }
      
    }

    public void TransitionToMap()
    {
        state = BattleStates.WON;
        var director = Tools.GetDirector();
        director.party.Clear();
        foreach(var unit in playerUnits)
        {
            director.party.Add(unit);
            DontDestroyOnLoad(unit);
            unit.gameObject.SetActive(false);
        }
        director.DisplayCharacterTab();
       // SceneManager.LoadScene(0);
    }


    public void DisplayEnemyIntent(Action action, Unit unit)
    {
        unit.intentUI.gameObject.SetActive(true);
        unit.intentUI.textMesh.text = action.ActionName;
        unit.intentUI.damageNums.text = (action.damage + unit.attackStat - action.target.defenseStat).ToString();
        unit.intentUI.target.text = action.target.unitName;
        unit.intentUI.action = action;
    }
    public static void SetStatChanges(Stat statToRaise, int AmountToRaise, bool multiplicative, Unit target)
    {
        var battleSystem = GameObject.FindObjectOfType<BattleSystem>();
        var popup = Instantiate(battleSystem.statPopUp, new Vector3(target.transform.position.x, target.transform.position.y + 2f, target.transform.position.z), Quaternion.identity);
        battleSystem.StartCoroutine(battleSystem.ChangeStat(statToRaise, AmountToRaise, multiplicative, target, popup));
        if (AmountToRaise < 0)
        {
            battleSystem.StartCoroutine(Tools.SmoothMove(popup, 0.01f, 60, 0, -0.005f));
        }
        else
        {
            battleSystem.StartCoroutine(Tools.SmoothMove(popup, 0.01f, 60, 0, 0.005f));
        }


    }
        public IEnumerator ChangeStat(Stat statToRaise, int AmountToRaise, bool multiplicative, Unit target, GameObject popup)
    {
       
        var number = popup.GetComponentInChildren<TextMeshProUGUI>();
        var particleSystem = target.GetComponent<ParticleSystem>();
        switch (statToRaise)
        {
         
            case Stat.ATK:
                if (!multiplicative)
                {
                    target.attackStat += AmountToRaise;
                }
                else
                {
                    target.attackStat *= AmountToRaise;
                }
                number.SetText(AmountToRaise.ToString());
                number.color = Color.red;
                particleSystem.startColor = Color.red;
                break;
            case Stat.DEF:
                if (!multiplicative)
                {
                    target.defenseStat += AmountToRaise;
                }
                else
                {
                    target.defenseStat *= AmountToRaise;
                }
                number.SetText(AmountToRaise.ToString());
                number.color = Color.blue;
                particleSystem.startColor = Color.blue;
                break;
            case Stat.SPD:
                if (!multiplicative)
                {
                    target.speedStat += AmountToRaise;
                }
                else
                {
                    target.speedStat *= AmountToRaise;
                }
                number.SetText(AmountToRaise.ToString());
                number.color = Color.yellow;
                particleSystem.startColor = Color.yellow;
                break;

        }
        if (AmountToRaise < 0)
        {
            particleSystem.gravityModifier = 10;
            var shape = particleSystem.shape;
            shape.position = new Vector3(shape.position.x, shape.position.y + 0.5f, shape.position.z);
        }
        particleSystem.Play();
        print("stats should be popping up");
        yield return new WaitForSeconds(0.7f);
        particleSystem.Stop();
        if (AmountToRaise < 0)
        {
            particleSystem.gravityModifier = 0;
            var shape = particleSystem.shape;
            shape.position = new Vector3(shape.position.x, shape.position.y - 0.5f, shape.position.z);
        }
        Destroy(popup);
        yield break;

    }
    void TurnBattleOrderOn()
    {
     
        foreach (var unit in Tools.GetAllUnits())
        {
            speedlist.Add(unit);
            print(unit.speedStat);
        }
        speedlist = speedlist.OrderBy(s => s.speedStat).ToList();
        speedlist.Reverse();
        int i = 0;
        foreach (var unit in speedlist)
        {
            Vector3 pos = new Vector3(BattleOrderpos.position.x + 4.2f * i, BattleOrderpos.position.y, 0);
            var battleOrder = Instantiate(BattleOrderImage, pos, canvas.transform.rotation);
            battleOrder.transform.SetParent(canvas.transform);
            battleOrder.transform.localScale = BattleOrderImage.transform.localScale;
            var unitName = battleOrder.GetComponent<BattleOrder>();
            unitName.textMesh.text = unit.unitName;
            if (unit.IsPlayerControlled)
            {
                unit.state = PlayerState.IDLE;
            }
            i++;
        }
    }

  
    public static void SetUIOff(Unit unit)
    {
        int i = 0;
        if (unit.skillUIs != null)
        {
            foreach (var skill in unit.skillUIs)
            {
                if(unit.state == PlayerState.DECIDING)
                {
                    unit.state = PlayerState.IDLE;
                }
                unit.skillUIs[i].SetActive(false);
                var actionContainer = unit.skillUIs[i].GetComponent<ActionContainer>();
                actionContainer.targetting = false;
                i++;
            }
        }
    }

    public static void SetUIOn(Unit unit)
    {
        int i = 0;
        //BattleSystem.SetUIOff();
        foreach(var x in Tools.GetAllUnits())
        {
            BattleSystem.SetUIOff(x);
        }
        foreach (var action in unit.actionList)
        {
            unit.state = PlayerState.DECIDING;
            unit.skillUIs[i].SetActive(true);
            var assignedAction = unit.skillUIs[i].GetComponent<ActionContainer>();
            var button = assignedAction.GetComponent<Button>();
            assignedAction.targetting = false;
            button.interactable = true;;
            assignedAction.action = action;
            assignedAction.damageNums.text = (action.damage + unit.attackStat).ToString();
            assignedAction.accuracyNums.text = action.accuracy.ToString();
            assignedAction.textMesh.text = action.ActionName;
            i++;
        }
    }

    public void AddAction(Action action)
    {
        var newAction = UnityEngine.Object.Instantiate(action);
        if (newAction.PriorityMove)
        {
            PriorityActions.Add(newAction);
        }
        else
        {
            ActionsToPerform.Add(newAction);
        }

    }
        public void StartBattlePhase()
        {

        StartCoroutine(BattleOrderImage.TransitionUIOut(0.06f));
        StartCoroutine(Tools.SmoothMove(BL.gameObject, 0.001f, 60, 0, -3.6f)); 
         foreach(var unit in Tools.GetAllUnits())
        {
            if(unit.intentUI != null)
            unit.intentUI.gameObject.SetActive(false);
            unit.state = PlayerState.BATTLING;
        }
        BattleLog.ClearBattleLog();
        BattleLog.DisableCharacterStats();
        BattleLog.ClearBattleText();
        StartCoroutine(PerformBattlePhase());
         state = BattleStates.BATTLE;
        }
    public IEnumerator PerformBattlePhase()
    {
        ActionsToPerform = ActionsToPerform.OrderBy(x => x.unit.speedStat).ToList();
        ActionsToPerform.Reverse();
        PriorityActions = PriorityActions.OrderBy(x => x.unit.speedStat).ToList();
        PriorityActions.Reverse();
        
            print("ITS ON!!!!");
        yield return new WaitForSeconds(1f);
        foreach (var action in PriorityActions)
        {
            if (action.unit != null && action.target != null)
            {
                action.OnActivated();
                yield return new WaitForSeconds(1f);
            }
        }
        foreach (var action in ActionsToPerform)
        {
            if (action.unit != null && action.target != null)
            {
                action.OnActivated();
                Debug.Log(action.speed);
                yield return new WaitForSeconds(1f);
            }
        }
        ActionsToPerform.Clear();
        ActionsToPerform = new List<Action>();
        speedlist.Clear();
        speedlist = new List<Unit>();
        PriorityActions.Clear();
        PriorityActions = new List<Action>();
        numOfUnits.Clear();
        playerUnits.Clear();
        enemyUnits.Clear();
        numOfReadyUnits.Clear();
        foreach(var unit in Tools.GetAllUnits()) { unit.DoBattlePhaseEnd(); }
        foreach(var bo in GameObject.FindObjectsOfType<BattleOrder>())
        {
            if(bo.name.Contains("Clone"))
            Destroy(bo);
        }
        yield return new WaitForSeconds(1f);
        Debug.Log("this is being called");
        StartDecisionPhase();
        state = BattleStates.DECISION_PHASE;
        yield break;

    }

    public void SetupHUD(Unit unit)
    {
        Healthbar health = hpslider.GetComponent<Healthbar>();
        health.unit = unit;
    }

   
}
