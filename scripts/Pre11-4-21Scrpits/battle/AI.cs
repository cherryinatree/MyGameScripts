using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI
{
    public enum ACTION { PATROL = 0, CHARGE = 1, ATTACK = 2 }
    public ACTION myCurrentAction = ACTION.PATROL;

    public CharacterMain target;
    public Character character = new Character();


    CharacterCube c = new CharacterCube();

    new protected void Start()
    {
        //base.Start();
        target = CharacterMain.theGoodGuys[0].GetComponent<CharacterMain>();
    }

    public void ChooseState(GameObject mySelf)
    {
        float targetDistance = Vector3.Distance(target.transform.position, mySelf.transform.position);

        if (targetDistance < 1.25f)
        {
            myCurrentAction = ACTION.ATTACK;
        }
        else
        {
            myCurrentAction = ACTION.CHARGE;
        }
    }

    public void ChooseTarget(GameObject mySelf)
    {
        foreach(GameObject a in CharacterMain.theGoodGuys)
        {
            if(target.GetComponent<CharacterMain>().my_cube == null)
            {
                target = a.GetComponent<CharacterMain>();
            }
            if (a.GetComponent<CharacterMain>().my_cube != null)
            {
                float targetDistance = Vector3.Distance(target.transform.position, mySelf.transform.position);
                float target2Distance = Vector3.Distance(a.transform.position, mySelf.transform.position);
                if (target.character.myStats.Threat * -targetDistance < a.GetComponent<CharacterMain>().character.myStats.Threat * -target2Distance)
                {
                    target = a.GetComponent<CharacterMain>();
                }
            }
        }
    }

    public int findPathDistance(cube start,cube end)
    {
        Queue<cube> countQueue = new Queue<cube>();
        start.gameObject.GetComponent<cubeTravel>().value = 0;
        List<cube> q1 = new List<cube>();
        List<cube> q2 = new List<cube>();


        neighborValues2(start, q1);
        int loopBreaker = 0;
        do
        {
            foreach (cube a in q1)
            {
                q2.Add(a);
            }
            q1.Clear();
            loopBreaker++;
            foreach (cube a in q2)
            {
                neighborValues2(a, q1);

            }

            if (loopBreaker == 10000)
            {
                Debug.Log("loop broke 1");
                break;
            }
            q2.Clear();
            
        } while (q1.Count != 0);

        q1.Clear();
        q1.Add(end);
        cube temp = end;



        loopBreaker = 0;
        do
        {
            loopBreaker++;
           // Debug.Log("next cube: " + temp.gameObject.GetComponent<cubeTravel>().cameFrom);
            temp = temp.gameObject.GetComponent<cubeTravel>().cameFrom;
            q1.Add(temp);

            if (loopBreaker == 10000)
            {
                Debug.Log("loop broke 2");
                break;
            }
        } while (temp.gameObject.GetComponent<cubeTravel>().cameFrom != temp.GetComponent<cube>());

        q1.Reverse();
        foreach (cube a in q1)
        {
            countQueue.Enqueue(a);
        }

        countQueue.Dequeue();
        
        foreach (GameObject a in cube.allTheCubes)
        {
            if (a.GetComponent<cubeTravel>())
            {
                a.GetComponent<cubeTravel>().resetValues();
            }
        }
        start.GetComponent<cubeTravel>().resetValues();


        return countQueue.Count;
    }

    public void neighborValues2(cube start, List<cube> q1)
    {
        CheckThisWay2(start, q1, Vector3.right);
        CheckThisWay2(start, q1, -Vector3.right);
        CheckThisWay2(start, q1, Vector3.forward);
        CheckThisWay2(start, q1, -Vector3.forward);


        start.gameObject.GetComponent<cubeTravel>().isVisited = true;
    }

    public void CheckThisWay2(cube start, List<cube> q1, Vector3 direction)
    {
        Collider[] tempPos1;
        Vector3 halfExtends = new Vector3(0.25f, 0.45f, 0.25f);
        tempPos1 = Physics.OverlapBox(start.transform.position + direction, halfExtends);
        foreach (Collider a in tempPos1)
        {
            if (a.CompareTag("Board") || a.CompareTag("badGuyStart") || a.CompareTag("StartCube") || a.CompareTag("Cube"))
            {
                if (a.gameObject.GetComponent<cubeTravel>().isVisited == false)
                {
                    float weight = Vector3.Distance(start.transform.position, a.transform.position);
                    if (a.gameObject.GetComponent<cubeTravel>().value > start.gameObject.GetComponent<cubeTravel>().value + weight)
                    {
                        a.gameObject.GetComponent<cubeTravel>().value = start.gameObject.GetComponent<cubeTravel>().value + weight;
                        a.gameObject.GetComponent<cubeTravel>().cameFrom = start;
                        q1.Add(a.gameObject.GetComponent<cube>());
                    }
                }
            }
        }
    }


    void findWhereToMove(Character thisGuy, Stats myStats)
    {
        c.FindNeighborCubes(thisGuy.my_cube.GetComponent<cube>(), cube.CUBEPHASE.MOVE, myStats.MoveSpeed);
        int dis1 = 10000;
        //float dis2 = 10000;
        cube moveTo = null;
        foreach (GameObject a in cube.ActionCubes)
        {
            int dis2 = findPathDistance(a.GetComponent<cube>(), target.my_cube.GetComponent<cube>());
            //dis2 = Vector3.Distance(a.transform.position, target.transform.position);
            if (dis2 < dis1)
            {

                dis1 = dis2;
                moveTo = a.GetComponent<cube>();
            }
        }
        if (target.my_cube != null) thisGuy.findPath(moveTo);
        c.ResetVariables();
    }

    public void BadGuyUpdate(Character thisGuy, Stats myStats, GameObject mySelf)
    {
       //Debug.Log(thisGuy.myState);
        if (thisGuy.myState == Character.BATTLESTATE.MYTURN)
        {
            ChooseState(mySelf);
            ChooseTarget(mySelf);
            thisGuy.myState += 1;
        }

        if (thisGuy.myState == Character.BATTLESTATE.ACTION1)
        {
            cube.ActionCubes.Clear();
            ChooseState(mySelf);
            ChooseTarget(mySelf);

            if (myCurrentAction == AI.ACTION.ATTACK)
            {
                if (target.my_cube != null) thisGuy.Attack(target.my_cube.GetComponent<cube>(), mySelf);
               
            }
            if (myCurrentAction == AI.ACTION.CHARGE)
            {
                c.FindNeighborCubes(thisGuy.my_cube.GetComponent<cube>(), cube.CUBEPHASE.MOVE, myStats.MoveSpeed);
                float dis1 = 10000;
                float dis2 = 10000;
                cube moveTo = null;
                foreach (GameObject a in cube.ActionCubes)
                {
                    dis2 = Vector3.Distance(a.transform.position, target.transform.position);
                    Debug.Log("dis1: " + dis1 + " dis2: " + dis2);
                    if (dis2 < dis1)
                    {
                        dis1 = dis2;
                        moveTo = a.GetComponent<cube>();
                    }
                }
                if (target.my_cube != null) thisGuy.findPath(moveTo);
                c.ResetVariables();

                findWhereToMove(thisGuy, myStats);
            }

            thisGuy.myState += 1;
        }

        if (thisGuy.myState == Character.BATTLESTATE.ACTION2)
        {
            cube.ActionCubes.Clear();
            ChooseState(mySelf);
            ChooseTarget(mySelf);

            if (myCurrentAction == AI.ACTION.ATTACK)
            {
                if (target.my_cube != null) thisGuy.Attack(target.my_cube.GetComponent<cube>(), mySelf);
                
            }
            if (myCurrentAction == AI.ACTION.CHARGE)
            {
                findWhereToMove(thisGuy, myStats);
            }
            thisGuy.myState += 1;
        }


        //Debug.Log(target);
        //Debug.Log(mySelf);
        thisGuy.MoveCharacter();
        mySelf.transform.rotation = Quaternion.Slerp(mySelf.transform.rotation,
        Quaternion.LookRotation(new Vector3(target.transform.position.x, mySelf.transform.position.y,
        target.transform.position.z) - mySelf.transform.position),
        2 * Time.deltaTime);
    }
}
