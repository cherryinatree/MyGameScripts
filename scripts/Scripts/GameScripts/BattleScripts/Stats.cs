using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GamingTools
{
    [System.Serializable]
    public class Stats
    {

        public string name;
        public string CharacterClass;
        public string statusEffect;
        public string picture;
        public float speed;
        public float attackRange;
        public int health;
        public int armorClass;
        public int MagicResistance;
        public int dodge;
        public int attackPower;
        public int magicPower;
        public int cover;
        public int level;
        public int actions;
        public int actionsLeft;
        public List<string> attacks;
        public List<string> spells;
        public List<string> items;

        public Stats()
        {
            attackRange = 5;
            speed = 20;
            actions = 2;
            health = 6;
            magicPower = 4;
            armorClass = 10;
            cover = 0;
            dodge = 2;
            attackPower = 2;
            MagicResistance = 10;
            level = 1;
            actionsLeft = actions;
            picture = "noRightsToImage1";
            name = "Naming Namerson";
            CharacterClass = "Wizard";
            attacks = new List<string>();
            spells = new List<string>();
            items = new List<string>();
            spells.Add("FireBall");
            spells.Add("FireBall");
            spells.Add("FireBall");
            spells.Add("FireBall");
            spells.Add("FireBall");
            items.Add("HealthPotion");
            items.Add("HealthPotion");
            items.Add("HealthPotion");
            attacks.Add("BasicAttack");
        }

    }
}
