﻿using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class Item : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite sprite;


    public event Action<Item> OnPickedUp;
    public event Action<Item> Removed;

    public virtual void OnPickup(Unit unit) 
    {
        OnPickedUp?.Invoke(this);  
    }

    public virtual void OnRemoved(Unit unit)
    {
        Removed?.Invoke(this);
    }
}
