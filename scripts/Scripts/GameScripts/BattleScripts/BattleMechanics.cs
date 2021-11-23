using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamingTools;
using Constants;

public static class BattleMechanics
{
    
   public enum AttackType
    {
        Magic = 0, MagicRange = 1, Attack = 2, Range = 3, Item = 4
    };

    private const float chanceRoll = 20;
    public static float HitChance(GameObject attacker, GameObject defender, AttackType type)
    {
        float chanceToHit = 0;
        Stats attackerStats = attacker.GetComponent<CharacterController>().myStats;
        Stats defenderStats = defender.GetComponent<CharacterController>().myStats;

        float hightDifference = attacker.transform.position.y - defender.transform.position.y;
        if(hightDifference < 0)
        {
            hightDifference = 0;
        }
        hightDifference = hightDifference / 5;

        if (type == AttackType.Attack)
        {
            chanceToHit = chanceRoll + attackerStats.attackPower - defenderStats.dodge - defenderStats.armorClass;
            chanceToHit = (chanceToHit / chanceRoll) * 100;
            return chanceToHit;
        }
        if (type == AttackType.Range)
        {
            chanceToHit = chanceRoll + attackerStats.attackPower + hightDifference - defenderStats.dodge - defenderStats.armorClass - defenderStats.cover;
            chanceToHit = (chanceToHit / chanceRoll) * 100;
            return chanceToHit;
        }
        if (type == AttackType.MagicRange)
        {
            chanceToHit = chanceRoll + attackerStats.attackPower + hightDifference - defenderStats.dodge - defenderStats.armorClass - defenderStats.cover;
            chanceToHit = (chanceToHit / chanceRoll) * 100;
            return chanceToHit;
        }
        if (type == AttackType.Magic)
        {
            chanceToHit = chanceRoll + attackerStats.attackPower - defenderStats.dodge - defenderStats.MagicResistance;
            chanceToHit = (chanceToHit / chanceRoll) * 100;
            return chanceToHit;
        }
        if (type == AttackType.Item)
        {
            chanceToHit = chanceRoll + attackerStats.attackPower - defenderStats.dodge - defenderStats.armorClass;
            chanceToHit = (chanceToHit / chanceRoll) * 100;
            return chanceToHit;
        }
        return chanceToHit;
    }

    public static void Attack(GameObject attacker, GameObject defender, AttackType type, string attackName)
    {

        attacker.GetComponent<CharacterController>().myStats.actionsLeft -= 1;
        float chance = HitChance(attacker, defender, type);

        int isItOne = Random.Range(1, 100);
        if(isItOne <= chance)
        {
            ProceedWithAttack(attacker, defender, type, attackName);
        }
        else
        {
            FailAttack(attacker, defender, type, attackName);
        }

    }

    private static void ProceedWithAttack(GameObject attacker, GameObject defender, AttackType type, string attackName)
    {
        int action = 0;
        if(type == AttackType.Attack)
        {
            for (int i = 0; i < ActionsList.Actions.GetLength(0); i++)
            {
                if (ActionsList.Actions[i, 0] == attackName)
                {
                    action = i;
                }
            }
            int damage = Random.Range(int.Parse(ActionsList.Actions[action, 4]), int.Parse(ActionsList.Actions[action, 5]));
            defender.GetComponent<CharacterController>().myStats.health -= damage;
            attacker.GetComponent<Animator>().SetTrigger(ActionsList.Actions[action, 8]);
            if (defender.GetComponent<CharacterController>().myStats.health < 0)
            {
                defender.GetComponent<CharacterController>().myStats.health = 0;
            }
        }
        UserInterface.ShowCharacterInfo(defender.GetComponent<CharacterController>().myStats);
    }

    private static void FailAttack(GameObject attacker, GameObject defender, AttackType type, string attackName)
    {
        Debug.Log("Miss");
    }
}
