using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIgeneral
{
    public string Foward = "W";
    public void Controls()
    {
        if (Input.GetButtonDown(Foward)) MoveForward();
          
    }
    public void MoveForward()
    {

    }
}
