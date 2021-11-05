using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Spell : MonoBehaviour
{
    public enum SPELLCLASS { HEALING = 0, RANGE = 1, MELEE = 2}
    public SPELLCLASS myClass;
    
    public string aName = null;
    public AudioClip aSound;
    public float spellLength = 2f;
    public Sprite myImage = null;
    public int spellID;

    private void Start()
    {
        Invoke("Die", spellLength);
    }

    private void Die()
    {
        Destroy(gameObject);
    }


}
