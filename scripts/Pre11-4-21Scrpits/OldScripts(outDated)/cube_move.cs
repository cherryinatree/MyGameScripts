/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class cube_move : cube
{

    public cube_move()
    {

    }

    public void FindNeighborCubes(cube cursor_cube, CUBEPHASE changePhaseTo, float distance)
    {/*
        CheckCubes(Vector3.forward, cursor_cube, changePhaseTo, distance);
        CheckCubes(-Vector3.forward, cursor_cube, changePhaseTo, distance);
        CheckCubes(Vector3.right, cursor_cube, changePhaseTo, distance);*/
      /*  CheckCubes(-Vector3.right, cursor_cube, changePhaseTo, distance);
    }

    public void FindSquareNeighborCubes(cube cursor_cube, CUBEPHASE changePhaseTo, float distance)
    { 
        CheckCubes(Vector3.forward, cursor_cube, changePhaseTo, distance);
        CheckCubes(-Vector3.forward, cursor_cube, changePhaseTo, distance);
        CheckCubes(Vector3.right, cursor_cube, changePhaseTo, distance);
        CheckCubes(-Vector3.right, cursor_cube, changePhaseTo, distance);
        CheckCubes(-Vector3.right + Vector3.forward, cursor_cube, changePhaseTo, distance);
        CheckCubes(-Vector3.right + -Vector3.forward, cursor_cube, changePhaseTo, distance);
        CheckCubes(Vector3.right + Vector3.forward, cursor_cube, changePhaseTo, distance);
        CheckCubes(Vector3.right + -Vector3.forward, cursor_cube, changePhaseTo, distance);
    }

  

    public void CheckCubes(Vector3 direction, cube cursor_cube, CUBEPHASE changePhaseTo, float distance)
    {
        Collider[] tempPosition;
        List<cube> cubeNeighbors = new List<cube>();
        List<cube> cubeNeighbors2 = new List<cube>();

        Vector3 halfExtends = new Vector3(0.25f, 0.45f, 0.25f);
        tempPosition = Physics.OverlapBox(cursor_cube.transform.position + direction, halfExtends);
        
        if (changePhaseTo == CUBEPHASE.MOVE)
        {
            cubeNeighbors = CheckForOccupancy(tempPosition, cubeNeighbors);
        }
        else
        {
            foreach (Collider a in tempPosition)
            {
                if (a.gameObject.CompareTag("Board") || a.gameObject.CompareTag("badGuyStart")
                    || a.gameObject.CompareTag("StartCube") || a.CompareTag("Cube"))
                {
                    cubeNeighbors.Add(a.gameObject.GetComponent<cube>());
                }
            }
        }


        Val(cursor_cube, cubeNeighbors, changePhaseTo);
        int loopBreaker = 0;
        
        for(int i = 0; i< distance; i++)
        {
            foreach (cube a in cubeNeighbors)
            {
                cubeNeighbors2.Add(a);
            }
            cubeNeighbors.Clear();
            loopBreaker++;
            foreach (cube a in cubeNeighbors2)
            {
                Val(a, cubeNeighbors, changePhaseTo);

            }

            if (loopBreaker == 1000)
            {
                Debug.Log("loop broke 1");
                break;
            }
            cubeNeighbors2.Clear();

        }// while (cubeNeighbors.Count != 0);
     /*   foreach (cube a in cubeNeighbors)
        {
             if (a.myPhase != changePhaseTo)
             {
            cubeNeighbors2.Add(a);
            a.myPhase = changePhaseTo;
            a.CubeColor();
        }
        }
        int counter = 1;
        if (counter < distance)
        {
            foreach (cube a in cubeNeighbors2)
            {
                    FindNeighborCubes(a, changePhaseTo, distance - 1);
                
            }
           // counter++;
        }*/

  /*  }

    public void Val(cube start, List<cube> q1, CUBEPHASE changePhaseTo)
    {
        Check(start, q1, Vector3.right, changePhaseTo);
        Check(start, q1, -Vector3.right, changePhaseTo);
        Check(start, q1, Vector3.forward, changePhaseTo);
        Check(start, q1, -Vector3.forward, changePhaseTo);
        
    }

    public void Check(cube start, List<cube> q1, Vector3 direction, CUBEPHASE changePhaseTo)
    {
        Collider[] tempPos1;
        Vector3 halfExtends = new Vector3(0.25f, 0.45f, 0.25f);
        tempPos1 = Physics.OverlapBox(start.transform.position + direction, halfExtends);


        List<cube> cubeNeighbors = new List<cube>();
        if (changePhaseTo == cube.CUBEPHASE.MOVE)
        {
            cubeNeighbors = CheckForOccupancy(tempPos1, cubeNeighbors);
        }
        else
        {
            foreach (Collider a in tempPos1)
            {
                Debug.Log(a);
                if (a != null)
                {
                    if (a.CompareTag("Board") || a.CompareTag("badGuyStart") || a.CompareTag("StartCube") || a.CompareTag("Cube"))
                    {
                        cubeNeighbors.Add(a.gameObject.GetComponent<cube>());
                    }
                }
            }
        }
        foreach (cube a in cubeNeighbors)
        {
            if (a.CompareTag("Board") || a.CompareTag("badGuyStart") || a.CompareTag("StartCube") || a.CompareTag("Cube"))
            {
              //  CheckForOccupancy

                if (a.myPhase != cube.CUBEPHASE.MOVE)
                {
                    a.myPhase = changePhaseTo;
                    a.CubeColor();

                    q1.Add(a);
                    
                }
            }
        }
    }




    int counter = 0;

    public List<cube> CheckForOccupancy(Collider[] tempPosition, List<cube> cubeNeighbors)
    {
        foreach (Collider a in tempPosition)
        {

            counter++;

            cube cube = null;
             if (a.CompareTag("Board") || a.CompareTag("badGuyStart") || a.CompareTag("StartCube") || a.CompareTag("Cube"))
             {
            cube = a.GetComponent<cube>();

            }
            if (cube != null)
            {

                foreach (GameObject guy in goodGuy.theGoodGuys)
                {
                    if (cube != null)
                    {

                        if (cube.gameObject == guy.GetComponent<goodGuy>().my_cube)
                        {
                            cube = null;
                        }
                    }
                }
                if (cube != null)
                {

                    foreach (GameObject guy in badGuy.theBadGuys)
                    {
                        if (cube != null && guy !=null)
                        {

                            if (cube.gameObject == guy.GetComponent<badGuy>().my_cube)
                            {
                                cube = null;
                            }
                        }
                    }

                    if (cube != null)
                    {
                        cubeNeighbors.Add(cube);
                    }

                }
            }
        }
        return cubeNeighbors;
    }


    /***************************************************************
     *                  How To Move!
     * 
     * 
     * This is called on by the Move Class for WASD input. It takes
     *  the vector3 direction and the current_cube the move class is
     *  focused on and checks if it can move in the direction it wants.
     *  If so it returns the cube to be updated as the current_cube in
     *  the move class.
     * 
     * 
     * *************************************************************/
    /*
    public cube MoveCube(Vector3 direction, cube cursor_cube)
    {
        // a collider array is used because we are testing for nearby colliders of other cubes 
        Collider[] tempPosition;
        // this lists the neighbors the cube finds
        List<cube> cubeNeighbors = new List<cube>();

        // the cube's property selected cube is turned true so it's no longer yellow
        // only the selected cube is yellow
        cursor_cube.selectedCube = false;

        Vector3 halfExtends = new Vector3(0.25f, 10.45f, 0.25f);
        tempPosition = Physics.OverlapBox(cursor_cube.transform.position + direction, halfExtends);
        foreach (Collider a in tempPosition)
        {
            cube cube = null;
            if (a.CompareTag("Board") || a.CompareTag("badGuyStart") || a.CompareTag("StartCube") || a.CompareTag("Cube"))
            {
                cube = a.GetComponent<cube>();
            }
            else if(a.CompareTag("Player"))
            {
                cube = a.GetComponent<Character>().my_cube.GetComponent<cube>();
            }
                if (cube != null)
                {

                    cursor_cube = cube;

                    // the cube's property selected cube is turned true so it becomes yellow
                    cube.selectedCube = true;
                }
            
        }


        return cursor_cube;
    }

    // checks if cursor cube is on a good guy and if he has moved
    public GameObject MoveGoodGuy(cube cursor_cube)
    {
        GameObject PC;
        PC = null;
        foreach (GameObject a in goodGuy.theGoodGuys)
        {

            if (a.GetComponent<goodGuy>().my_cube == cursor_cube.gameObject 
                && a.GetComponent<goodGuy>().myState != goodGuy.BATTLESTATE.TURNOVER)
            {
                PC = a.gameObject;
            }
        }
        return PC;
    }



    public bool CheckValidMove(cube cursor_cube)
    {
        bool valid = false;

        if (cursor_cube.myPhase == CUBEPHASE.MOVE)
        {

            valid = true;
        }
        ResetVariables();
        return valid;
    }

    public void ResetVariables()
    {

        foreach (GameObject a in ActionCubes)
        {
            a.GetComponent<cube>().myPhase = CUBEPHASE.NORMAL;
            a.GetComponent<cube>().CubeColor();
        }

    }

}*/
