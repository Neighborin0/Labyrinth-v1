using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChestNodeObject : MonoBehaviour
{
    public List<ItemDisplay> itemDisplays;

    private void Start()
    {
        GetRandomItem();
    }
    public void GetRandomItem()
    {
        var director = GameObject.FindObjectOfType<Director>();
        var chosenItem = director.itemDatabase[UnityEngine.Random.Range(0, director.itemDatabase.Count)];
        itemDisplays[0].item = chosenItem;
        print(chosenItem.itemName);
        StartCoroutine(itemDisplays[0].TransitionIn(0.15f));
    }
   
}
