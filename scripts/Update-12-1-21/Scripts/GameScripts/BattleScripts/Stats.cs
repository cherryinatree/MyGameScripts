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
        public int dexterity;
        public int attackPower;
        public int magicPower;
        public int cover;
        public int level;
        public int expirence;
        public int actions;
        public int actionsLeft;
        public int maxHealth;

        public List<string> attacks;
        public List<string> spells;
        public List<string> items;

        public Stats()
        {
            attackRange = 5;
            speed = 20;
            actions = 2;
            health = 6;
            maxHealth = 6;
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

        public Stats(string TheCharacterClass)
        {
            attacks = new List<string>();
            spells = new List<string>();
            items = new List<string>();

            if (TheCharacterClass == "Fighter")
            {
                speed = Constants.BaseStatsPC.Fighter.Floats[0];
                attackRange = Constants.BaseStatsPC.Fighter.Floats[1];

                int i = 0;
                health = Constants.BaseStatsPC.Fighter.Ints[i];
                maxHealth = Constants.BaseStatsPC.Fighter.Ints[i];
                i++;
                armorClass = Constants.BaseStatsPC.Fighter.Ints[i];
                i++;
                MagicResistance = Constants.BaseStatsPC.Fighter.Ints[i];
                i++;
                dodge = Constants.BaseStatsPC.Fighter.Ints[i];
                i++;
                dexterity = Constants.BaseStatsPC.Fighter.Ints[i];
                i++;
                attackPower = Constants.BaseStatsPC.Fighter.Ints[i];
                i++;
                magicPower = Constants.BaseStatsPC.Fighter.Ints[i];
                i++;
                cover = Constants.BaseStatsPC.Fighter.Ints[i];
                i++;
                level = Constants.BaseStatsPC.Fighter.Ints[i];
                i++;
                expirence = Constants.BaseStatsPC.Fighter.Ints[i];
                i++;
                actions = Constants.BaseStatsPC.Fighter.Ints[i];
                i++;
                actionsLeft = Constants.BaseStatsPC.Fighter.Ints[i];


                name = Constants.BaseStatsPC.Fighter.Strings[0];
                CharacterClass = Constants.BaseStatsPC.Fighter.Strings[1];
                statusEffect = Constants.BaseStatsPC.Fighter.Strings[2];
                picture = Constants.BaseStatsPC.Fighter.Strings[3];

                foreach (var item in Constants.BaseStatsPC.Fighter.Actions)
                {
                    attacks.Add(item);
                }
                foreach (var item in Constants.BaseStatsPC.Fighter.Spells)
                {
                    spells.Add(item);
                }
                foreach (var item in Constants.BaseStatsPC.Fighter.Actions)
                {
                    items.Add(item);
                }
            }
            if (TheCharacterClass == "Wizard")
            {
                speed = Constants.BaseStatsPC.Wizard.Floats[0];
                attackRange = Constants.BaseStatsPC.Wizard.Floats[1];

                int i = 0;
                health = Constants.BaseStatsPC.Wizard.Ints[i];
                maxHealth = Constants.BaseStatsPC.Wizard.Ints[i];
                i++;
                armorClass = Constants.BaseStatsPC.Wizard.Ints[i];
                i++;
                MagicResistance = Constants.BaseStatsPC.Wizard.Ints[i];
                i++;
                dodge = Constants.BaseStatsPC.Wizard.Ints[i];
                i++;
                dexterity = Constants.BaseStatsPC.Wizard.Ints[i];
                i++;
                attackPower = Constants.BaseStatsPC.Wizard.Ints[i];
                i++;
                magicPower = Constants.BaseStatsPC.Wizard.Ints[i];
                i++;
                cover = Constants.BaseStatsPC.Wizard.Ints[i];
                i++;
                level = Constants.BaseStatsPC.Wizard.Ints[i];
                i++;
                expirence = Constants.BaseStatsPC.Wizard.Ints[i];
                i++;
                actions = Constants.BaseStatsPC.Wizard.Ints[i];
                i++;
                actionsLeft = Constants.BaseStatsPC.Wizard.Ints[i];


                name = Constants.BaseStatsPC.Wizard.Strings[0];
                CharacterClass = Constants.BaseStatsPC.Wizard.Strings[1];
                statusEffect = Constants.BaseStatsPC.Wizard.Strings[2];
                picture = Constants.BaseStatsPC.Wizard.Strings[3];

                foreach (var item in Constants.BaseStatsPC.Wizard.Actions)
                {
                    attacks.Add(item);
                }
                foreach (var item in Constants.BaseStatsPC.Wizard.Spells)
                {
                    spells.Add(item);
                }
                foreach (var item in Constants.BaseStatsPC.Wizard.Actions)
                {
                    items.Add(item);
                }
            }
            if (TheCharacterClass == "Rogue")
            {
                speed = Constants.BaseStatsPC.Rogue.Floats[0];
                attackRange = Constants.BaseStatsPC.Rogue.Floats[1];

                int i = 0;
                health = Constants.BaseStatsPC.Rogue.Ints[i];
                maxHealth = Constants.BaseStatsPC.Rogue.Ints[i];
                i++;
                armorClass = Constants.BaseStatsPC.Rogue.Ints[i];
                i++;
                MagicResistance = Constants.BaseStatsPC.Rogue.Ints[i];
                i++;
                dodge = Constants.BaseStatsPC.Rogue.Ints[i];
                i++;
                dexterity = Constants.BaseStatsPC.Rogue.Ints[i];
                i++;
                attackPower = Constants.BaseStatsPC.Rogue.Ints[i];
                i++;
                magicPower = Constants.BaseStatsPC.Rogue.Ints[i];
                i++;
                cover = Constants.BaseStatsPC.Rogue.Ints[i];
                i++;
                level = Constants.BaseStatsPC.Rogue.Ints[i];
                i++;
                expirence = Constants.BaseStatsPC.Rogue.Ints[i];
                i++;
                actions = Constants.BaseStatsPC.Rogue.Ints[i];
                i++;
                actionsLeft = Constants.BaseStatsPC.Rogue.Ints[i];


                name = Constants.BaseStatsPC.Rogue.Strings[0];
                CharacterClass = Constants.BaseStatsPC.Rogue.Strings[1];
                statusEffect = Constants.BaseStatsPC.Rogue.Strings[2];
                picture = Constants.BaseStatsPC.Rogue.Strings[3];

                foreach (var item in Constants.BaseStatsPC.Rogue.Actions)
                {
                    attacks.Add(item);
                }
                foreach (var item in Constants.BaseStatsPC.Rogue.Spells)
                {
                    spells.Add(item);
                }
                foreach (var item in Constants.BaseStatsPC.Rogue.Actions)
                {
                    items.Add(item);
                }
            }
        }

    }
}
