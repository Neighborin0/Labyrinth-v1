using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterNode : MonoBehaviour
{
    public void Transition()
    {
        SceneManager.LoadScene(2);
    }

}
