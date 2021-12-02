using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TurnController 
{
    public static void NextTurn()
    {
        if (BattleSingleton.Instance.currentTurn >= BattleSingleton.Instance.Combatants.Count - 1)
        {
            BattleSingleton.Instance.currentTurn = 0;
        }
        else
        {
            BattleSingleton.Instance.currentTurn++;
        }
        foreach (GameObject item in BattleSingleton.Instance.ourTurn)
        {
            item.GetComponent<CharacterController>().myStats.actionsLeft = item.GetComponent<CharacterController>().myStats.actions;
        }
        BattleSingleton.Instance.ourTurn = BattleSingleton.Instance.Combatants[BattleSingleton.Instance.currentTurn];
    }
}
