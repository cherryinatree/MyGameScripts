using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class goodGuy : MonoBehaviour
{
    public Transform self;

    public static List<GameObject> theGoodGuys = new List<GameObject>();
    public static List<Stats> theGoodGuysStats = new List<Stats>();

    public float attackRange = 1f;
    public Character character;
    public GameObject my_cube;

    //public bool moved = false;
    
    


    private void Awake()
    {
        character.mySelf = gameObject;
        character.myState = Character.BATTLESTATE.ACTION1;
        GameObject[] theCubes = GameObject.FindGameObjectsWithTag("StartCube");
        character.my_cube = theCubes[0];
        character.my_cube.tag = "Cube";

        theGoodGuys.Add(gameObject);
        theGoodGuysStats.Add(character.myStats);
        self = gameObject.transform;
        //LevelModifier();
        

    }

    public static int statCount = 0;
    // Start is called before the first frame update
    new void Start()
    {


        //base.Start();
        self.transform.position = character.my_cube.transform.position;
        self.transform.position = new Vector3(self.transform.position.x, 0.5f, self.transform.position.z);


        try
        {
            Save save = new Save();
            GameData stat = save.LoadGameData(Save.LastLoadedFile);
            if (stat != null)
            {
                character.myStats.LoadStats(stat, statCount);
                statCount++;
            }
        }
        catch (FileNotFoundException e)
        {
            Debug.Log(e);
        }
    }


    private void Update()
    {
        character.MoveCharacter();

    }
}
