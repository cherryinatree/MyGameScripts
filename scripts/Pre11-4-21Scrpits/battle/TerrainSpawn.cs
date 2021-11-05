using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSpawn : MonoBehaviour
{
    public GameObject my_cube;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] theCubes = GameObject.FindGameObjectsWithTag("terrainGround");
        my_cube = theCubes[0];
        my_cube.tag = "Cube";
        gameObject.transform.position = my_cube.transform.position;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0.5f, gameObject.transform.position.z);
    }
    
}
