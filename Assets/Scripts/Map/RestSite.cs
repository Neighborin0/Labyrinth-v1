using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class RestSite : MonoBehaviour
{
    [SerializeField] public List<Transform> characterRestPos;


    private void Start()
    {
        for(int i = 0; i <= Tools.GetDirector().party.Count - 1; i++)
        {
            Tools.GetDirector().party[i].gameObject.SetActive(true);
            Tools.GetDirector().party[i].transform.position = characterRestPos[i].position;

        }
    }

    public void StartRest()
    {
        StartCoroutine(Rest());
    }

    public void StartTrain()
    {
        StartCoroutine(Train());
    }
    public IEnumerator Train()
    {
        var director = Tools.GetDirector();
        for (int i = 0; i <= director.party.Count - 1; i++)
        {
            director.LevelUp(director.party[i]);
            Tools.GetDirector().party[i].gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }
    public IEnumerator Rest()
    {
        var director = Tools.GetDirector();
        for (int i = 0; i <= director.party.Count - 1; i++)
        {
           director.party[i].currentHP = director.party[i].maxHP;
           Tools.GetDirector().party[i].gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }

}
