using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Constants
{
    public class ActionsList
    {
        // [0] Action name,[1] Target Type,[2] RangeType,
        // [3] Range,[4] min damage,[5] max damage,[6] ActionType,
        // [7] Action Level,[8] Action Animation

        public static string[,] Actions = new string[2, 9]
        {
        {"Heal", "Friend", "Close", "5", "1", "4", "Healing", "1", "Attack1" },
        {"BasicAttack", "Enemy", "Close", "5", "1", "4", "Damage", "1", "Attack1" }
        };
    }
}
