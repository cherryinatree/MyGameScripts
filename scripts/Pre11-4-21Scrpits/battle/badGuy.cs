using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class badGuy : MonoBehaviour
{
/*
    //public AI myBrain;
    
    public static List<GameObject> theBadGuys = new List<GameObject>();
    public GameObject my_cube;
    

    public void Awake()
    {
        character.mySelf = gameObject;
        GameObject[] theCubes = GameObject.FindGameObjectsWithTag("badGuyStart");
        my_cube = theCubes[0];
        my_cube.tag = "Cube";

       // myBrain = gameObject.GetComponent<AI>();
        theBadGuys.Add(gameObject);
        //LevelModifier();
    }

    new protected void Start()
    {
        //base.Start();
        gameObject.transform.position = my_cube.transform.position;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0.5f, gameObject.transform.position.z);

       
    }

    

    public void Update()
    {
            if (myState == BATTLESTATE.MYTURN)
            {
                ChooseState();
                ChooseTarget();
                myState += 1;
            }

            if (myState == BATTLESTATE.ACTION1)
            {
                ActionCubes.Clear();
                ChooseState();
                ChooseTarget();
                if (myCurrentAction == AI.ACTION.ATTACK)
                {
                    if (target.my_cube != null) Attack(target.my_cube.GetComponent<cube>(), gameObject);
                }
                if (myCurrentAction == AI.ACTION.CHARGE)
            {
                 FindNeighborCubes(my_cube.GetComponent<cube>(), CUBEPHASE.MOVE, MoveSpeed);
                 float dis1 = 10000;
                 float dis2 = 10000;
                 cube moveTo = null;
                 foreach (GameObject a in ActionCubes)
                 {
                     dis2 = Vector3.Distance(a.transform.position, target.transform.position);
                     Debug.Log("dis1: " + dis1 + " dis2: " + dis2);
                     if (dis2 < dis1)
                     {
                         dis1 = dis2;
                         moveTo = a.GetComponent<cube>();
                     }
                 }
                 if (target.my_cube != null) findPath(moveTo);
                 ResetVariables();

               findWhereToMove();
            }
                
            myState += 1;
            }

            if (myState == BATTLESTATE.ACTION2)
        {
            ActionCubes.Clear();
            ChooseState();
                ChooseTarget();

                if (myCurrentAction == AI.ACTION.ATTACK)
                {
                    Attack(target.GetComponent<cube>(), gameObject);
                }
                if (myCurrentAction == AI.ACTION.CHARGE)
                {
                findWhereToMove();
            }
            myState += 1;
            }
            
        

            MoveCharacter();
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation,
            Quaternion.LookRotation(new Vector3(target.transform.position.x, gameObject.transform.position.y,
            target.transform.position.z) - gameObject.transform.position),
            2 * Time.deltaTime);
    }
    


    void findWhereToMove()
    {
        FindNeighborCubes(my_cube.GetComponent<cube>(), CUBEPHASE.MOVE, MoveSpeed);
        int dis1 = 10000;
        //float dis2 = 10000;
        cube moveTo = null;
        foreach (GameObject a in ActionCubes)
        {
            int dis2 = findPathDistance(a.GetComponent<cube>(), target.my_cube.GetComponent<cube>());
            //dis2 = Vector3.Distance(a.transform.position, target.transform.position);
            if (dis2 < dis1)
            {

                dis1 = dis2;
                moveTo = a.GetComponent<cube>();
            }
        }
        if (target.my_cube != null) findPath(moveTo);
        ResetVariables();
    }*/
}
