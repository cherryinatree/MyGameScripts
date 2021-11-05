using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class InvokeNonMono : MonoBehaviour
{
    private GameObject me;

    public void InvokeMe(GameObject mySelf)
    {
        me = mySelf;
        Debug.Log("its me: " + me);
        Invoke("ObjectDeath", 1f);
    }

    public void ObjectDeath()
    {
        CharacterMain.theBadGuys.Remove(me);
        CharacterMain.theGoodGuys.Remove(me);
    }
}
