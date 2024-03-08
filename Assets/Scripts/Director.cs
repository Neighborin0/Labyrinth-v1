using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class Director : MonoBehaviour
{
    public List<Unit> party;
    public List<Unit> characterdatabase;
    public List<Item> itemDatabase;
    public List<Action> actionDatabase;
    public BattleLog BL;
    public CharacterTab characterTab;
    public DirectorInfo directorInfo;
    public static int LabyrinthLVL;
    public GameObject characterSlotpos;
    public CharacterSlot characterSlot;
    public GameObject TabGrid;
     void Awake()
    {
        if (GameObject.FindGameObjectsWithTag(gameObject.tag).Length > 1)
        {
            Destroy(this);
        }
        else
        {
            
            DontDestroyOnLoad(gameObject);
            foreach(var unit in party.ToList())
            {
                var startingUnit = Instantiate(unit);
                party.Remove(unit);
                DontDestroyOnLoad(startingUnit);
                party.Add(startingUnit);            
                startingUnit.gameObject.SetActive(false);
            }   
        }
       // Tools.ClearAllCharacterSlots();
        //CreateCharacterSlots(party);
    }

     void Update()
    {
        //directorInfo.text.text = "LV." + LabyrinthLVL.ToString();
    }

    public void CreateCharacterSlots(List<Unit> units)
    {
        int i = 0;
        if (units != null)
        {
            foreach (var x in units)
            {
                if (x != null)
                {
                    CharacterSlot newcharacterSlot = Instantiate(characterSlot, Vector3.zero, Quaternion.identity);
                    var healthbar = newcharacterSlot.slider;
                    healthbar.maxValue = x.maxHP;
                    if (x.currentHP < 1)
                    {
                        healthbar.value = x.maxHP;
                    }
                    else
                    {
                        healthbar.value = x.currentHP;
                    }
                    newcharacterSlot.healthNumbers.text = healthbar.value.ToString() + "/" + x.maxHP.ToString();
                    newcharacterSlot.transform.SetParent(characterSlotpos.transform, false);
                    //newcharacterSlot.transform.localScale = characterSlot.transform.localScale;
                    newcharacterSlot.namePlate.text = x.unitName;
                    i++;
                }
            }
            
        }
    }   
    public static void AddUnitToParty(string unitName)
    {
        //var unitToAdd = Instantiate(characterdatabase.Where(obj => obj.name == unitName).SingleOrDefault());
        var director = GameObject.FindObjectOfType<Director>();
        var unitToAdd = Instantiate(director.characterdatabase.Where(obj => obj.name == unitName).SingleOrDefault());
        DontDestroyOnLoad(unitToAdd);
        unitToAdd.gameObject.SetActive(false);
        director.party.Add(unitToAdd);
    }
    public void DisplayCharacterTab()
    {
        foreach (var unit in party)
        {
            CharacterTab CT = Instantiate(characterTab, Vector3.zero, Quaternion.identity);
            CT.gameObject.transform.SetParent(TabGrid.transform, false);
            //StartCoroutine(Tools.SmoothMove(CT, 0.001f, 60, 0, 16f));
            foreach (var x in characterTab.buttons)
            {
                x.interactable = true;
            }
            CT.unit = unit;
        }
    }

    public void LevelUp(Unit unit)
    {
        LabyrinthLVL += 1;
        unit.attackStat += 1;
        unit.speedStat += 1;
        unit.defenseStat += 1;
        unit.maxHP += 2;
    }
}
