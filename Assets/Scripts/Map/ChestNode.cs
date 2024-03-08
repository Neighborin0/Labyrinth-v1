using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChestNode : MonoBehaviour
{
    public void Transition()
    {
        SceneManager.LoadScene(3);
    }
   
}
