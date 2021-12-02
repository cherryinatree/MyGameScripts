using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamingTools;

public static class CharacterCreator
{
    public enum CHARACTERCLASS {
    Fighter= 0, Wizard = 1, Rogue = 2
    }

    public static Stats CreateCharacter(CHARACTERCLASS theClass)
    {
        if (theClass == CHARACTERCLASS.Fighter)
        {
            Stats theStat = new Stats("Fighter");
            return theStat;
        }
        if (theClass == CHARACTERCLASS.Wizard)
        {
            Stats theStat = new Stats("Wizard");
            return theStat;
        }
        if (theClass == CHARACTERCLASS.Rogue)
        {
            Stats theStat = new Stats("Rogue");
            return theStat;
        }
        Stats theStats = new Stats();
        return theStats;
    }
}
