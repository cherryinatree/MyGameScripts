using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMain : MonoBehaviour
{
    private Transform self;

    public static List<GameObject> theGoodGuys = new List<GameObject>();
    public static List<GameObject> theGoodGuysForSavingStats = new List<GameObject>();
    public static List<GameObject> theBadGuys = new List<GameObject>();
    public static List<Stats> theGoodGuysStats = new List<Stats>();
    private GameObject[] theCubes;

    private bool goodGuy;

    public float attackRange = 1f;
    public Character character;
    public Stats myStats;
    public GameObject my_cube;

    public AI myAI;


    public void CharacterUploadStats(bool goodOrBad, Stats loadMyStats)
    {
        //Debug.Log(goodOrBad);
        AmIGood(goodOrBad);
        loadStats(loadMyStats);
    }

    private void AmIGood(bool goodOrBad)
    {
        goodGuy = goodOrBad;
        if(goodGuy == true)
        {
            theGoodGuys.Add(gameObject);
            theGoodGuysForSavingStats.Add(gameObject);
        }
        else
        {
            theBadGuys.Add(gameObject);
        }

    }

    private void loadStats(Stats loadMyStats)
    {
        character = new Character();
        myStats = loadMyStats;
        character.mySelf = gameObject;
        self = gameObject.transform;
        //LevelModifier();

        Debug.Log(goodGuy);
        if (goodGuy == true)
        {
            character.myState = Combat.BATTLESTATE.ACTION1;
            theCubes = GameObject.FindGameObjectsWithTag("StartCube");
            theGoodGuysStats.Add(myStats);
        }
        else
        {
            Debug.Log(1);
            character.mySelf = gameObject;
            theCubes = GameObject.FindGameObjectsWithTag("badGuyStart");
            my_cube = theCubes[0];
        }

        character.my_cube = my_cube;
        character.my_cube.tag = "Cube";
    }

    public void HealSelf()
    {
        Cursor.visible = false;
        HealthPoints += 25;
        if (HealthPoints >= myStats.MaxHealthPoints)
        {
            HealthPoints = myStats.MaxHealthPoints;
        }
        GameObject clone;
        foreach (GameObject a in character.allMySpells)
        {
            if (a.GetComponent<Spell>().myClass == Spell.SPELLCLASS.HEALING)
            {

                clone = GameObject.Instantiate(a, gameObject.transform.position, a.transform.rotation);
            }
        }
        character.myState += 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (goodGuy)
        {
            character.MoveCharacter();
            my_cube = character.my_cube;
        }
        else
        {
            myAI.BadGuyUpdate(character,myStats,gameObject);
            my_cube = character.my_cube;
        }

        /***********************************************************************************
        *
        *       Temporary for testing, Remove as soon as no longer needed
        *
        **********************************************************************************/
                
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            myStats.MeleeDamage += 20;
        }
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            myStats.MeleeDamage -= 20;
        }

    }


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

                Animator anime = gameObject.GetComponent<Animator>();
                anime.SetTrigger("death");
                my_cube = null;

                Invoke("ObjectDeath", 1f);
                //InvokeNonMono invokeMe = new InvokeNonMono();
                //invokeMe.InvokeMe(gameObject);
                character.myState = Character.BATTLESTATE.DEAD;
            }
        }
    }


    public void ObjectDeath()
    {
        CharacterMain.theBadGuys.Remove(gameObject);
        CharacterMain.theGoodGuys.Remove(gameObject);
    }


    new protected void Start()
    {
        if (goodGuy)
        {

        }
        else
        {
            myAI = new AI();
            badGuyStart();
        }

    }

    private void badGuyStart()
    {
        character.mySelf = gameObject;
        myAI.target = CharacterMain.theGoodGuys[0].GetComponent<CharacterMain>();
        //LevelModifier();

        gameObject.transform.position = my_cube.transform.position;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0.5f, gameObject.transform.position.z);
    }
}
