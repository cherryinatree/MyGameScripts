using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

[System.Serializable]
public class GameData
{   
    private float[] _level;
    private float[] _experience;
    private float[] _HealthPoints;
    private float[] _MaxHealthPoints;
    private int[] _MoveSpeed;
    private float[] _meleeAttack;
    private float[] _meleeDamage;
    private float[] _magicDamage;
    private float[] _armor;
    private float[] _Threat;
    private float[] _weapon;
    private float[] _strength;
    private float[] _dex;
    private float[] _constitution;
    private float[] _will;
    private float[] _intelligence;
    private float[] _chrisma;
    private float[] _speed;
    private string[] _myName;
    private int _homeTownWidth;
    private int _homeTownLength;
    //private List<Stats> _playerCharacters;
    public Stats char1;
    public Stats char2;
    public Stats char3;

    public GameData(GameData data)
    {
        homeTownWidth = data.homeTownWidth;
        homeTownLength = data.homeTownLength;
    }
    
    int x = 0;
    public GameData()
    {
        homeTownWidth = 10;
        homeTownLength = 10;
    }

    public void UpdateStats(Stats one, Stats two, Stats three)
    {
        char1 = one;
        char2 = two;
        char3 = three;
    }

    public void CreateNewGameData()
    {
        x = 0;
        int size = 3;
        Stats guy = new Stats();
        char1 = guy;
        Stats guy1 = new Stats();
        char2 = guy1;
        Stats guy2 = new Stats();
        char3 = guy2;
        homeTownWidth = 10;
        homeTownLength = 10;
    }

    public string[] MyName
    {
        get { return _myName; }
        set { _myName = value; }
    }
    public float[] level
    {
        get { return _level; }
        set { _level = value; }
    }
    public float[] experience
    {
        get { return _experience; }
        set { _experience = value; }
    }
    public float[] HealthPoints
    {
        get { return _HealthPoints; }
        set { _HealthPoints = value; }
    }
    public float[] MaxHealthPoints
    {
        get { return _MaxHealthPoints; }
        set { _MaxHealthPoints = value; }
    }
    public int[] MoveSpeed
    {
        get { return _MoveSpeed; }
        set { _MoveSpeed = value; }
    }
    public float[] meleeAttack
    {
        get { return _meleeAttack; }
        set { _meleeAttack = value; }
    }
    public float[] meleeDamage
    {
        get { return _meleeDamage; }
        set { _meleeDamage = value; }
    }
    public float[] magicDamage
    {
        get { return _magicDamage; }
        set { _magicDamage = value; }
    }
    public float[] armor
    {
        get { return _armor; }
        set { _armor = value; }
    }
    public float[] Threat
    {
        get { return _Threat; }
        set { _Threat = value; }
    }
    public float[] weapon
    {
        get { return _weapon; }
        set { _weapon = value; }
    }
    public float[] strength
    {
        get { return _strength; }
        set { _strength = value; }
    }
    public float[] dex
    {
        get { return _dex; }
        set { _dex = value; }
    }
    public float[] constitution
    {
        get { return _constitution; }
        set { _constitution = value; }
    }
    public float[] will
    {
        get { return _will; }
        set { _will = value; }
    }
    public float[] intelligence
    {
        get { return _intelligence; }
        set { _intelligence = value; }
    }
    public float[] chrisma
    {
        get { return _chrisma; }
        set { _chrisma = value; }
    }
    public float[] speed
    {
        get { return _speed; }
        set { _speed = value; }
    }
    public int homeTownWidth
    {
        get { return _homeTownWidth; }
        set { _homeTownWidth = value; }
    }
    public int homeTownLength
    {
        get { return _homeTownLength; }
        set { _homeTownLength = value; }
    }
    /* public List<Stats> playerCharacters
     {
         get { return _playerCharacters; }
         set { _playerCharacters = value; }
     }*/




    /* level = data.level;
     experience = data.experience;
     HealthPoints = data.HealthPoints;
     MaxHealthPoints = data.MaxHealthPoints;
     MoveSpeed = data.MoveSpeed;
     meleeAttack = data.meleeAttack;
     meleeDamage = data.meleeDamage;
     magicDamage = data.magicDamage;
     armor = data.armor;
     Threat = data.Threat;
     weapon = data.weapon;
     strength = data.strength;
     dex = data.dex;
     constitution = data.constitution;
     will = data.will;
     intelligence = data.intelligence;
     chrisma = data.chrisma;
     speed = data.speed;*/
    // playerCharacters = data.playerCharacters;



    //int size = goodGuy.theGoodGuys.Count;
    //Debug.Log("size: " + size);
    /* level = new float[size];
     experience = new float[size];
     HealthPoints = new float[size];
     MaxHealthPoints = new float[size];
     MoveSpeed = new int[size];
     meleeAttack = new float[size];
     meleeDamage = new float[size];
     magicDamage = new float[size];
     armor = new float[size];
     Threat = new float[size];
     weapon = new float[size];
     strength = new float[size];
     dex = new float[size];
     constitution = new float[size];
     will = new float[size];
     intelligence = new float[size];
     chrisma = new float[size];
     speed = new float[size];*/



    /*  foreach (GameObject guy in goodGuy.theGoodGuys)
      {
          level[x] = guy.GetComponent<Stats>().Level;
          experience[x] = guy.GetComponent<Stats>().Experience;
          HealthPoints[x] = guy.GetComponent<Stats>().HealthPoints;
          MaxHealthPoints[x] = guy.GetComponent<Stats>().MaxHealthPoints;
          MoveSpeed[x] = guy.GetComponent<Stats>().MoveSpeed;
          meleeAttack[x] = guy.GetComponent<Stats>().MeleeAttack;
          meleeDamage[x] = guy.GetComponent<Stats>().MeleeDamage;
          magicDamage[x] = guy.GetComponent<Stats>().MagicDamage;
          armor[x] = guy.GetComponent<Stats>().Armor;
          Threat[x] = guy.GetComponent<Stats>().Threat;
          weapon[x] = guy.GetComponent<Stats>().Weapon;
          strength[x] = guy.GetComponent<Stats>().Strength;
          dex[x] = guy.GetComponent<Stats>().Dex;
          constitution[x] = guy.GetComponent<Stats>().Constitution;
          will[x] = guy.GetComponent<Stats>().Will;
          intelligence[x] = guy.GetComponent<Stats>().Intelligence;
          chrisma[x] = guy.GetComponent<Stats>().Chrisma;
          speed[x] = guy.GetComponent<Stats>().Speed;
          x++;
      }*/


    //List<goodGuy> guys = new List<goodGuy>();
    //playerCharacters = new List<Stats>();
    /*for (int i = 0; i<size; i++)
    {
        Stats guy = new Stats();
        playerCharacters.Add(guy);
    }*/
    /*
    level = new float[size];
    experience = new float[size];
    HealthPoints = new float[size];
    MaxHealthPoints = new float[size];
    MoveSpeed = new int[size];
    meleeAttack = new float[size];
    meleeDamage = new float[size];
    magicDamage = new float[size];
    armor = new float[size];
    Threat = new float[size];
    weapon = new float[size];
    strength = new float[size];
    dex = new float[size];
    constitution = new float[size];
    will = new float[size];
    intelligence = new float[size];
    chrisma = new float[size];
    speed = new float[size];*/



    /*  foreach (goodGuy guy in playerCharacters)
      {
          level[x] = guy.Level;
          experience[x] = guy.Experience;
          HealthPoints[x] = guy.HealthPoints;
          MaxHealthPoints[x] = guy.MaxHealthPoints;
          MoveSpeed[x] = guy.MoveSpeed;
          meleeAttack[x] = guy.MeleeAttack;
          meleeDamage[x] = guy.MeleeDamage;
          magicDamage[x] = guy.MagicDamage;
          armor[x] = guy.Armor;
          Threat[x] = guy.Threat;
          weapon[x] = guy.Weapon;
          strength[x] = guy.Strength;
          dex[x] = guy.Dex;
          constitution[x] = guy.Constitution;
          will[x] = guy.Will;
          intelligence[x] = guy.Intelligence;
          chrisma[x] = guy.Chrisma;
          speed[x] = guy.Speed;
          x++;
      }*/
}
