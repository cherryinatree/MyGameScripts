using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpellList
{
    public List<GameObject> allTheSpells = new List<GameObject>();
    public List<GameObject> allMySpells = new List<GameObject>();
    public Stats myStats = new Stats();


    private void Start()
    {
        if (allTheSpells.Count != 0)
        {
            foreach (GameObject a in allTheSpells)
            {
                int wheresA = allTheSpells.IndexOf(a);
                GameObject temp = allTheSpells[a.GetComponent<Spell>().spellID];
                allTheSpells[a.GetComponent<Spell>().spellID] = a;
                allTheSpells[wheresA] = temp;
            }
            foreach (int a in myStats.ListOfSpellId)
            {
                allMySpells.Add(allTheSpells[0]);
            }
        }
    }

    

}
