using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeTravel : MonoBehaviour
{
    public float value = 1000f;
    public bool isVisited = false;
    public cube cameFrom = null;

    private void Awake()
    {
        cameFrom = gameObject.GetComponent<cube>();
    }

    public void resetValues()
    {
        value = 1000f;
        isVisited = false;
        cameFrom = gameObject.GetComponent<cube>();
    }
}
