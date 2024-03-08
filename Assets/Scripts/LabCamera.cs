using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabCamera : MonoBehaviour
{
    private Camera cam;
    private bool HasReachedMax = false;
    void Start()
    {
         cam = this.GetComponent<Camera>();
    }

     void LateUpdate()
    {
        float step = 0.2f * Time.deltaTime;
       transform.position = Vector3.LerpUnclamped(transform.position, new Vector3(transform.position.x + UnityEngine.Random.Range(-4f, 4), transform.position.y + UnityEngine.Random.Range(-4f , 4), transform.position.z), step);
       
    }
}
