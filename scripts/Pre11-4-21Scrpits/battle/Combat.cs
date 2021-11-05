using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combat : SpellList
{

    Animation animationAttack = null;
    public Text popupText = null;
    Transform canvas;
    public GameObject loadedSpell;

    public enum BATTLESTATE
    {
        WAITING = 0, MYTURN = 1, ACTION1 = 2, Animating = 3,
        ACTION2 = 4, Animating2 = 5, OUTOFMOVES = 6, TURNOVER = 7, DEAD = 8
    };
    public BATTLESTATE myState = BATTLESTATE.TURNOVER;

    private void Start()
    {
        canvas = GameObject.Find("Canvas").transform;
    }

    public void MagicAttack(cube cursor_cube, GameObject attacker)
    {

        myState += 1;
        Collider[] tempPosition = new Collider[1];
        tempPosition[0] = cursor_cube.GetComponent<Collider>();
        List<cube> cubeNeighbors = new List<cube>();
        cubeNeighbors = cursor_cube.GetComponent<CharacterCube>().CheckForOccupancy(tempPosition, cubeNeighbors);
        if (cubeNeighbors != null)
        {
            foreach (GameObject guy in CharacterMain.theBadGuys)
            {
                if (guy != null)
                {
                    if (cursor_cube.gameObject == guy.GetComponent<CharacterMain>().my_cube)
                    {
                        bool tryAttack = hitAttempt(guy);
                        if (tryAttack)
                        {
                            AudioSource myAudio = attacker.GetComponent<AudioSource>();
                            myAudio.Play();
                            animateAttack(guy, attacker);
                            damage(guy, attacker);
                            GameObject clone;
                            clone = GameObject.Instantiate(loadedSpell, guy.transform.position, allMySpells[1].transform.rotation);
                        }
                        else
                        {
                            animateMiss(guy);
                        }
                    }
                }
            }
            foreach (GameObject guy in CharacterMain.theGoodGuys)
            {
                if (guy != null)
                {
                    if (cursor_cube.gameObject == guy.GetComponent<CharacterMain>().my_cube)
                    {
                        bool tryAttack = hitAttempt(guy);
                        if (tryAttack)
                        {
                            AudioSource myAudio = guy.GetComponent<AudioSource>();
                            myAudio.Play();
                            animateAttack(guy, attacker);
                            damage(guy, attacker);
                        }
                        else
                        {
                            animateMiss(guy);
                        }
                    }
                }
            }
        }

    }


    public void Attack(cube cursor_cube, GameObject attacker)
    {

        myState += 1;
        //Collider[] tempPosition = new Collider[1];
        //tempPosition[0] = cursor_cube.GetComponent<Collider>();
        //List<cube> cubeNeighbors = new List<cube>();
        //cubeNeighbors = cursor_cube.GetComponent<CharacterCube>().CheckForOccupancy(tempPosition, cubeNeighbors);
        // if(cubeNeighbors != null)
        // {
        Debug.Log(CharacterMain.theBadGuys.Count);
            foreach (GameObject guy in CharacterMain.theBadGuys)
            {
                if (guy != null && guy.GetComponent<CharacterMain>().my_cube != null)
            {
                Debug.Log(guy.GetComponent<CharacterMain>().my_cube);
                if (cursor_cube.gameObject == guy.GetComponent<CharacterMain>().my_cube)
                    {
                        bool tryAttack = hitAttempt(guy);
                        if (tryAttack)
                        {
                            AudioSource myAudio = attacker.GetComponent<AudioSource>();
                            myAudio.Play();
                            animateAttack(guy, attacker);
                            damage(guy, attacker);
                        }
                        else
                        {
                            animateMiss(guy);
                        }
                    }
                }
            }
            foreach (GameObject guy in CharacterMain.theGoodGuys)
            {
                if (guy != null && guy.GetComponent<CharacterMain>().my_cube != null)
                {
                    if (cursor_cube.gameObject == guy.GetComponent<CharacterMain>().my_cube)
                    {
                        bool tryAttack = hitAttempt(guy);
                        if (tryAttack)
                        {
                            AudioSource myAudio = guy.GetComponent<AudioSource>();
                            myAudio.Play();
                            animateAttack(guy, attacker);
                            damage(guy, attacker);
                        }
                        else
                        {
                            animateMiss(guy);
                        }
                    }
                }
            }
        //}
        
    }

    public bool hitAttempt(GameObject guy)
    {
        float attack = Random.Range(guy.GetComponent<CharacterMain>().myStats.MeleeAttack, guy.GetComponent<CharacterMain>().myStats.MeleeAttack * 2);

        if (attack >  guy.GetComponent<CharacterMain>().myStats.Armor)
        {

            return true;
        }

        CombatText(guy, "Miss");
        return false;
    }
    
    public void animateAttack(GameObject guy, GameObject attacker)
    {
        Animator anime = attacker.GetComponent<Animator>();
        anime.SetTrigger("attacking");
        anime = guy.GetComponent<Animator>();

        anime.SetTrigger("isHit");

    }

    public void animateMiss(GameObject guy)
    {

    }

    public void damage(GameObject guy, GameObject attacker)
    {
        float damageDone = Random.Range(attacker.GetComponent<CharacterMain>().myStats.MeleeDamage, attacker.GetComponent<CharacterMain>().myStats.MeleeDamage * 2);
        damageDone = Mathf.Round(damageDone);
        guy.GetComponent<CharacterMain>().HealthPoints -= damageDone;
        //CombatText(guy, damageDone.ToString());
        if(guy.GetComponent<CharacterMain>().HealthPoints <= 0)
        {
            attacker.GetComponent<CharacterMain>().myStats.Experience += guy.GetComponent<CharacterMain>().myStats.Experience;
        }
    }

    public void Magicdamage(GameObject guy, GameObject attacker)
    {
        float damageDone = Random.Range(attacker.GetComponent<CharacterMain>().myStats.MagicDamage, attacker.GetComponent<CharacterMain>().myStats.MagicDamage * 2);
        damageDone = Mathf.Round(damageDone);
        guy.GetComponent<CharacterMain>().HealthPoints -= damageDone;
       // CombatText(guy, damageDone.ToString());
        if (guy.GetComponent<CharacterMain>().HealthPoints <= 0)
        {
            attacker.GetComponent<CharacterMain>().myStats.Experience += guy.GetComponent<CharacterMain>().myStats.Experience;
        }
    }

    void CombatText(GameObject target, string battleText)
    {

        Text instance = GameObject.Instantiate<Text>(popupText);
        instance.GetComponent<QuickText>().QuickTextSays(battleText);
        instance.transform.SetParent(GameObject.Find("Canvas").transform);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(target.transform.position);
        instance.transform.position = screenPosition;
    }

}


