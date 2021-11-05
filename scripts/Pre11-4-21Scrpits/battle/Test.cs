using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Test
{

    // public Stats myStats;

    // public GameObject my_cube;
   /* public enum BATTLESTATE
    {
        WAITING = 0, MYTURN = 1, ACTION1 = 2, Animating = 3,
        ACTION2 = 4, Animating2 = 5, OUTOFMOVES = 6, TURNOVER = 7, DEAD = 8
    };
    public BATTLESTATE myState = BATTLESTATE.TURNOVER;*/

    // public GameObject DeathParticlesPrefab = null;
   // private Transform ThisTransform = null;
    //public bool ShouldDestroyOnDeath = true;
    // public GameObject mySelf;


    // [SerializeField]
    private float _level = 1f;
    // [SerializeField]
    private float _experience = 0f;

    // declares the variable that will be the health
    //  [SerializeField]
    public float _HealthPoints = 100f;
    // [SerializeField]
    private float _MaxHealthPoints = 100f;
    //  [SerializeField]
    private int _MoveSpeed = 10;
    //  [SerializeField]
    private float _meleeAttack = 15f;
    //  [SerializeField]
    private float _meleeDamage = 25f;
    //  [SerializeField]
    private float _magicDamage = 50f;
    //   [SerializeField]
    private float _armor = 7f;
    //  [SerializeField]
    private float _Threat = 100f;
    //  [SerializeField]
    private float _weapon = 10f;
    //  [SerializeField]
    private float _strength = 4f;
    //  [SerializeField]
    private float _dex = 100f;
    //  [SerializeField]
    private float _constitution = 100f;
    //  [SerializeField]
    private float _will = 100f;
    //   [SerializeField]
    private float _intelligence = 100f;
    //   [SerializeField]
    private float _chrisma = 100f;
    //  [SerializeField]
    private float _speed = 100f;

    //  [SerializeField]
    private int[] _listOfSpellId = { 0 };



    [SerializeField]
    public Test(Test loader)
    {
        _level = loader._level;
        _experience = loader._experience;
        _HealthPoints = loader._HealthPoints;
        _MaxHealthPoints = loader._MaxHealthPoints;
        _MoveSpeed = loader._MoveSpeed;
        _meleeAttack = loader._meleeAttack;
        _meleeDamage = loader._meleeDamage;
        _magicDamage = loader._magicDamage;
        _armor = loader._armor;
        _Threat = loader._Threat;
        _weapon = loader._weapon;
        _strength = loader._strength;
        _dex = loader._dex;
        _constitution = loader._constitution;
        _will = loader._will;
        _intelligence = loader._intelligence;
        _chrisma = loader._chrisma;
        _speed = loader._speed;
        _listOfSpellId = loader._listOfSpellId;

    }

    public void LoadStats(GameData loader, int whichCharacter)
    {
        Debug.Log(whichCharacter);
        // Level = loader.level[whichCharacter];
        _experience = loader.experience[whichCharacter];
        _HealthPoints = loader.HealthPoints[whichCharacter];
        _MaxHealthPoints = loader.MaxHealthPoints[whichCharacter];
        _MoveSpeed = loader.MoveSpeed[whichCharacter];
        _meleeAttack = loader.meleeAttack[whichCharacter];
        _meleeDamage = loader.meleeDamage[whichCharacter];
        _magicDamage = loader.magicDamage[whichCharacter];
        _armor = loader.armor[whichCharacter];
        _Threat = loader.Threat[whichCharacter];
        _weapon = loader.weapon[whichCharacter];
        _strength = loader.strength[whichCharacter];
        _dex = loader.dex[whichCharacter];
        _constitution = loader.constitution[whichCharacter];
        _will = loader.will[whichCharacter];
        _intelligence = loader.intelligence[whichCharacter];
        _chrisma = loader.chrisma[whichCharacter];
        _speed = loader.speed[whichCharacter];

    }

    public Test()
    {

    }
    
        // Start is called before the first frame update
        void Start()
        {
            // gets the transform of the object this script is attached to
           // ThisTransform = mySelf.GetComponent<Transform>();
        }
    

        public float Weapon
        {
            get { return _weapon; }
            set { _weapon = value; }
        }
        public float MaxHealthPoints
        {
            get { return _MaxHealthPoints; }
            set { _MaxHealthPoints = value; }
        }
        public int MoveSpeed
        {
            get { return _MoveSpeed; }
            set { _MoveSpeed = value; }
        }

        public float Threat
        {
            get { return _Threat; }
            set { _Threat = value; }
        }

        public float Level
        {
            get { return _level; }
            set { _level = value; }
        }

        public float Experience
        {
            get { return _experience; }
            set { _experience = value; }
        }
        public float MeleeAttack
        {
            get { return _meleeAttack; }
            set { _meleeAttack = value; }
        }
        public float MeleeDamage
        {
            get { return _meleeDamage; }
            set { _meleeDamage = value; }
        }
        public float MagicDamage
        {
            get { return _magicDamage; }
            set { _magicDamage = value; }
        }
        public float Strength
        {
            get { return _strength; }
            set { _strength = value; }
        }
        public float Dex
        {
            get { return _dex; }
            set { _dex = value; }
        }
        public float Constitution
        {
            get { return _constitution; }
            set { _constitution = value; }
        }
        public float Will
        {
            get { return _will; }
            set { _will = value; }
        }
        public float Intelligence
        {
            get { return _intelligence; }
            set { _intelligence = value; }
        }
        public float Chrisma
        {
            get { return _chrisma; }
            set { _chrisma = value; }
        }
        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }
        public float Armor
        {
            get { return _armor; }
            set { _armor = value; }
        }
        public int[] ListOfSpellId
        {
            get { return _listOfSpellId; }
            set { _listOfSpellId = value; }
        }
       /* public Stats theStats
        {
            get { return this; }
        }*/
}

