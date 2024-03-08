using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class MapController : MonoBehaviour
{
    void Awake()
    {
        StartCoroutine(LoadSlots());

    }

    public IEnumerator LoadSlots()
    {
        yield return new WaitForSeconds(0.0001f);
        var d = Tools.GetDirector();
        Tools.ClearAllCharacterSlots();
        d.CreateCharacterSlots(d.party);
    }
}
