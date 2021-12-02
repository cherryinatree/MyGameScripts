using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableNPC : MonoBehaviour
{

    public int id;
    // Start is called before the first frame update
    void Start()
    {

        GameEvents.current.onTalkToNPC += OnTalkToNPC;
    }

    public void OnTalkToNPC(int id)
    {
        GamingTools.SceneController.ChangeScene("BattleScene");
    }
}
