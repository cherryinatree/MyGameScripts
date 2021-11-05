using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class badGuyTurnController : MonoBehaviour
{

   public static Queue<GameObject> turnQueue = new Queue<GameObject>();


    void Start()
    {
        
        turnQueue.Enqueue(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
       if (turnQueue.Count == 0) return;

        if (turnQueue.Peek().GetComponent<CharacterMain>().character.myState == Combat.BATTLESTATE.WAITING)
        {
             turnQueue.Peek().GetComponent<CharacterMain>().character.myState = Combat.BATTLESTATE.MYTURN;
              
        }else if (turnQueue.Peek().GetComponent<CharacterMain>().character.myState == Combat.BATTLESTATE.OUTOFMOVES)
        {

            GameObject temp = turnQueue.Dequeue();
            //temp.GetComponent<CharacterMain>().myState = badGuy.BATTLESTATE.TURNOVER;
            turnQueue.Enqueue(temp);
            int count = 0;
            foreach(GameObject a in CharacterMain.theBadGuys)
            {
                if(a.GetComponent<CharacterMain>().character.myState >= Combat.BATTLESTATE.OUTOFMOVES) count++;
            }

            if (count == CharacterMain.theBadGuys.Count)
            {
                UserInterface.whosTurn = UserInterface.TURN.PLAYERTURN;
            }
        }
        else if(turnQueue.Peek().GetComponent<CharacterMain>().character.myState == Combat.BATTLESTATE.DEAD)
        {

            turnQueue.Dequeue();
        }


    }

    public static void StartEnemyTurn()
    {
        if (turnQueue.Count == 0) return;
        UserInterface.whosTurn = UserInterface.TURN.ENEMYTURN;
        turnQueue.Peek().GetComponent<CharacterMain>().character.myState = Combat.BATTLESTATE.MYTURN;
     }
}
