using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class map_maker
{
    public GameObject oneSquare;
    public GameObject TerrainSquare;
    public GameObject FillerSquare;
    public GameObject StartOneSquare;
    public GameObject BadStartOneSquare;
    public GameObject Enemy1;
    public GameObject[] Terrain1;
    public GameObject Knight;
    public int width = 10;
    public int length = 10;
    private int numberOfEnemies = 1;
    public int totalItems = 2;
    public int[] Xs;
    public int[] Ys;
    public int[,] terrainLoacations;
    public List<int> raisedGround;

    private int raisedCount = 0;
    private float groundHeight = 0f;





    public map_maker(GameData data)
    {
        LoadVariables(data);
        GatherPrefabs();
        MakeTheMap();
    }


    // Constructor for no added variables.
    public map_maker()
    {
        LoadVariables();
        GatherPrefabs();
        MakeTheMap();
    }


    private void GatherPrefabs()
    {
        oneSquare = (GameObject)Resources.Load("prefabs/Ground/Cube", typeof(GameObject));
        TerrainSquare = (GameObject)Resources.Load("prefabs/Ground/TerrainCube", typeof(GameObject));
        FillerSquare = (GameObject)Resources.Load("prefabs/Ground/fillerCube", typeof(GameObject));
        StartOneSquare = (GameObject)Resources.Load("prefabs/Ground/startingCube", typeof(GameObject));
        BadStartOneSquare = (GameObject)Resources.Load("prefabs/Ground/badCube", typeof(GameObject));
        Enemy1 = (GameObject)Resources.Load("prefabs/Enemies/theZombie", typeof(GameObject));
        Terrain1[0] = (GameObject)Resources.Load("prefabs/Enviorment/TreeGreen", typeof(GameObject));
        Terrain1[1] = (GameObject)Resources.Load("prefabs/Enviorment/TreeGreen", typeof(GameObject));
        Knight = (GameObject)Resources.Load("prefabs/GoodGuys/knight_1", typeof(GameObject));
    }

    private void LoadVariables()
    {
        Terrain1 = new GameObject[2];
        terrainLoacations = new int[2, 2];
        raisedGround = new List<int>();
        Xs = new int[] { 3, 4, 5 };
        Ys = new int[] { 3, 4, 5 };
    }

    private void LoadVariables(GameData data)
    {
        Terrain1 = new GameObject[2];
        terrainLoacations = new int[2, 2];
        raisedGround = new List<int>();
        Xs = new int[] { 3, 4, 5 };
        Ys = new int[] { 3, 4, 5 };
    }

    public void MakeTheGoodGuys(GameData data)
    {
        /*  foreach(Character guy in data.playerCharacters)
          {

              GameObject clone;
              clone = GameObject.Instantiate(guy.myPrefab, new Vector3(5f, 2f, 5f), Quaternion.identity);
              clone.GetComponent<goodGuy>().character = guy;
          }*/

        GameObject[] theCubes = GameObject.FindGameObjectsWithTag("StartCube");

        GameObject clone;
        clone = GameObject.Instantiate(Knight, new Vector3(theCubes[0].transform.position.x,
            theCubes[0].transform.position.y+0.5f, theCubes[0].transform.position.z), Quaternion.identity);
        clone.GetComponent<CharacterMain>().my_cube = theCubes[0];
        clone.GetComponent<CharacterMain>().CharacterUploadStats(true, data.char1);
        GameObject clone1;
        clone1 = GameObject.Instantiate(Knight, new Vector3(theCubes[1].transform.position.x,
            theCubes[1].transform.position.y + 0.5f, theCubes[1].transform.position.z), Quaternion.identity);
        clone1.GetComponent<CharacterMain>().my_cube = theCubes[1];
        clone1.GetComponent<CharacterMain>().CharacterUploadStats(true, data.char2);
        GameObject clone2;
        clone2 = GameObject.Instantiate(Knight, new Vector3(theCubes[2].transform.position.x,
            theCubes[2].transform.position.y + 0.5f, theCubes[2].transform.position.z), Quaternion.identity);
        clone2.GetComponent<CharacterMain>().my_cube = theCubes[2];
        clone2.GetComponent<CharacterMain>().CharacterUploadStats(true, data.char3);
        Debug.Log(data.char1);
    }


    private void MakeTheMap()
    {
        terrainLoacations = new int[totalItems, 2];
        for (int items = 0; items < totalItems; items++)
        {
            terrainLoacations[items, 0] = Xs[items];
            terrainLoacations[items, 1] = Ys[items];
        }
        for (float x = 0; x < length; x++)
        {
            for (float i = 0; i < width; i++)
            {
                groundHeight = 0f;
                foreach (int a in raisedGround)
                {
                    if (a == raisedCount)
                    {
                        groundHeight += 0.5f;
                    }
                }
                raisedCount++;
                bool terrainHere = false;
                for (int r = 0; r < terrainLoacations.GetLength(0); r++)
                {
                    if (terrainLoacations[r, 0] == x && terrainLoacations[r, 1] == i)
                    {
                        var newSquare = GameObject.Instantiate(TerrainSquare, new Vector3(x, groundHeight, i), Quaternion.identity);
                        newSquare.transform.parent = GameObject.Find("GameMaster").transform;
                        terrainHere = true;
                        for (float high = groundHeight; high > 1f; high--)
                        {
                            newSquare = GameObject.Instantiate(FillerSquare, new Vector3(x, high - 1, i), Quaternion.identity);
                            newSquare.transform.parent = GameObject.Find("GameMaster").transform;

                        }
                    }
                }
                if (!terrainHere)
                {
                    var newSquare = GameObject.Instantiate(oneSquare, new Vector3(x, groundHeight, i), Quaternion.identity);
                    newSquare.transform.parent = GameObject.Find("GameMaster").transform;
                    for (float high = groundHeight; high > 0.9f; high--)
                    {

                        newSquare = GameObject.Instantiate(FillerSquare, new Vector3(x, high - 1, i), Quaternion.identity);
                        newSquare.transform.parent = GameObject.Find("GameMaster").transform;
                    }
                }
            }
        }
        for (float i = 0; i < width; i++)
        {
            var newSquare = GameObject.Instantiate(StartOneSquare, new Vector3(i, 0f, 0 - 1), Quaternion.identity);
            newSquare.transform.parent = GameObject.Find("GameMaster").transform;

        }
        for (float i = 0; i < width; i++)
        {
            var newSquare = GameObject.Instantiate(BadStartOneSquare, new Vector3(i, 0f, width), Quaternion.identity);
            newSquare.transform.parent = GameObject.Find("GameMaster").transform;
        }/*
        for (float i = 0; i < numberOfEnemies; i++)
        {
            GameObject clone;
            clone = GameObject.Instantiate(Enemy1, new Vector3(5f, 2f, 5f), Quaternion.identity);
        }*/
        for (int i = 0; i < totalItems; i++)
        {
            GameObject clone;
            if (i < Terrain1.Length)
            {
                clone = GameObject.Instantiate(Terrain1[i], new Vector3(5f, 2f, 5f), Quaternion.identity);
            }
            else
            {
                clone = GameObject.Instantiate(Terrain1[0], new Vector3(5f, 2f, 5f), Quaternion.identity);
            }
        }
    }

    public void MakeTheBadGuys(float numberOfBadGuys)
    {

        for (float i = 0; i < numberOfBadGuys; i++)
        {
            GameObject clone;
            clone = GameObject.Instantiate(Enemy1, new Vector3(5f, 2f, 5f), Quaternion.identity);
            clone.GetComponent<CharacterMain>().CharacterUploadStats(false, new Stats());
        }
    }
    
}
