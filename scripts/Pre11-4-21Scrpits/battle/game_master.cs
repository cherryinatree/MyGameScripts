using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class game_master : MonoBehaviour
{
    bool turnTimer = false;
    private void Start()
    {
        Time.timeScale = 1.0f;
        Time.timeScale *= 3;
    }


    private void Update()
    {
        if(turnTimer == false && UserInterface.whosTurn == UserInterface.TURN.ENEMYTURN)
        {
            turnTimer = true;
            Invoke("TurnTimer", 20f);

        }
    }

    void TurnTimer()
    {
        UserInterface.whosTurn = UserInterface.TURN.PLAYERTURN;
        turnTimer = false;
    }
}



