using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character : Combat
{
    public enum FACEDIRECTION { LEFT = 0, FORWARD =1, RIGHT = 2, BACKWARD = 3 };
    public FACEDIRECTION Facing = FACEDIRECTION.FORWARD;

     public GameObject my_cube;

    protected Queue<cube> moveQueue = new Queue<cube>();
    public GameObject myPrefab;
    public GameObject mySelf;


    protected void Start()
    {

        myPrefab = (GameObject)Resources.Load("prefabs/GoodGuys/knight_1", typeof(GameObject));
        if (mySelf.transform.rotation.y <= 0.25 && mySelf.transform.rotation.y > -0.25) Facing = FACEDIRECTION.FORWARD;
        if (mySelf.transform.rotation.y <= -0.25 && mySelf.transform.rotation.y > -0.8) Facing = FACEDIRECTION.LEFT;
        if (mySelf.transform.rotation.y <= 1.1 && mySelf.transform.rotation.y > 0.8) Facing = FACEDIRECTION.BACKWARD;
        if (mySelf.transform.rotation.y <= 0.8 && mySelf.transform.rotation.y > 0.25) Facing = FACEDIRECTION.RIGHT;
    }
    /*
    protected void LevelModifier()
    {

        myStats.MaxHealthPoints += myStats.Level * 25;
        HealthPoints += myStats.Level * 25;
        myStats.MeleeAttack += myStats.Level;
        myStats.MeleeDamage += myStats.Level * 3;
        myStats.MagicDamage += myStats.Level * 5;
    }*/
    /*
    public float HealthPoints
    {
        get
        {   // returns how much health is left on a get request
            return myStats._HealthPoints;
        }
        // set the health
        set
        {
            // sets it to the value from the set request
            myStats._HealthPoints = value;
            //if health is 0 or less it destorys the object
            if (myStats._HealthPoints <= 0)
            {

                Animator anime = mySelf.GetComponent<Animator>();
                anime.SetTrigger("death");
                my_cube = null;

                //Invoke("objectDeath", 1f);
                InvokeNonMono invokeMe = new InvokeNonMono();
                invokeMe.InvokeMe(mySelf);
                myState = BATTLESTATE.DEAD;
            }
        }
    }

    public void objectDeath()
    {
        CharacterMain.theBadGuys.Remove(mySelf);
        CharacterMain.theGoodGuys.Remove(mySelf);
    }
    */

    public void findPath(cube end)
    {

        cube start = my_cube.GetComponent<cube>();
        start.gameObject.GetComponent<cubeTravel>().value = 0;
        List<cube> q1 = new List<cube>();
        List<cube> q2 = new List<cube>();
        

        neighborValues(start, q1);
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
                neighborValues(a, q1);

            }
       
            if (loopBreaker == 1000)
            {
                Debug.Log("loop broke 1");
                break;
            }
            q2.Clear();

        } while(q1.Count!=0);

        q1.Clear();
        q1.Add(end);
        cube temp = end;

        
        loopBreaker = 0;
            do
            {
                loopBreaker++;

            //  Debug.Log("next cube: " + temp.gameObject.GetComponent<cubeTravel>().cameFrom);
            temp = temp.gameObject.GetComponent<cubeTravel>().cameFrom;
                q1.Add(temp);

                if (loopBreaker == 1000)
                {
                    Debug.Log("loop broke 2");
                    break;
                }
            } while (temp.gameObject.GetComponent<cubeTravel>().cameFrom != temp.GetComponent<cube>());

            q1.Reverse();
            foreach (cube a in q1)
            {
                moveQueue.Enqueue(a);
            }

            moveQueue.Dequeue();

            foreach (GameObject a in cube.ActionCubes)
            {

                a.GetComponent<cubeTravel>().resetValues();
            }
            start.GetComponent<cubeTravel>().resetValues();
    }

    public void neighborValues(cube start, List<cube> q1)
    {
        CheckThisWay(start, q1, Vector3.right);
        CheckThisWay(start, q1, -Vector3.right);
        CheckThisWay(start, q1, Vector3.forward);
        CheckThisWay(start, q1, -Vector3.forward);


        start.gameObject.GetComponent<cubeTravel>().isVisited = true;
    }

    public void CheckThisWay(cube start, List<cube> q1, Vector3 direction)
    {
        Collider[] tempPos1;
        Vector3 halfExtends = new Vector3(0.25f, 0.45f, 0.25f);
        tempPos1 = Physics.OverlapBox(start.transform.position + direction, halfExtends);
        foreach (Collider a in tempPos1)
        {
            if (a.CompareTag("Board") || a.CompareTag("badGuyStart") || a.CompareTag("StartCube") || a.CompareTag("Cube"))
            {
                if (a.gameObject.GetComponent<cube>().myPhase == cube.CUBEPHASE.MOVE && a.gameObject.GetComponent<cubeTravel>().isVisited == false)
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

    public void MoveCharacter()
    {
        if (moveQueue.Count != 0)
        {

            float distance = Vector3.Distance(new Vector3(mySelf.transform.position.x, 0.5f, mySelf.transform.position.z),
                      new Vector3(moveQueue.Peek().transform.position.x, 0.5f, moveQueue.Peek().transform.position.z));

            if (distance > 0.15f)
            {
                Animator anime = mySelf.GetComponent<Animator>();
                anime.SetBool("isWalking", true);
                mySelf.transform.position = new Vector3(mySelf.transform.position.x, moveQueue.Peek().transform.position.y+0.5f, mySelf.transform.position.z);

                mySelf.transform.rotation = Quaternion.LookRotation(new Vector3(moveQueue.Peek().transform.position.x, 0f,
                    moveQueue.Peek().transform.position.z) - new Vector3(mySelf.transform.position.x, 0f, mySelf.transform.position.z));

                //gameObject.transform.rotation = Quaternion.LookRotation(Vector3.right);
                mySelf.transform.position += mySelf.transform.forward * 0.4f * Time.deltaTime;
                distance = Vector3.Distance(new Vector3(mySelf.transform.position.x, 0.5f, mySelf.transform.position.z),
                   new Vector3(moveQueue.Peek().transform.position.x, 0.5f, moveQueue.Peek().transform.position.z));
                if (distance <= 0.15f)
                {
                    mySelf.transform.position = new Vector3(moveQueue.Peek().transform.position.x,
                        moveQueue.Peek().transform.position.y + 0.5f, moveQueue.Peek().transform.position.z);
                    anime = mySelf.GetComponent<Animator>();
                    anime.SetBool("isWalking", false);
                    my_cube = moveQueue.Dequeue().gameObject;
                    if (moveQueue.Count == 0)
                    {
                        myState += 1;
                    }

                }
            }
            if (mySelf.transform.rotation.y <= 0.25 && mySelf.transform.rotation.y > -0.25) Facing = FACEDIRECTION.FORWARD;
            if (mySelf.transform.rotation.y <= -0.25 && mySelf.transform.rotation.y > -0.8) Facing = FACEDIRECTION.LEFT;
            if (mySelf.transform.rotation.y <= 1.1 && mySelf.transform.rotation.y > 0.8) Facing = FACEDIRECTION.BACKWARD;
            if (mySelf.transform.rotation.y <= 0.8 && mySelf.transform.rotation.y > 0.25) Facing = FACEDIRECTION.RIGHT;
        }
        

    }
}
