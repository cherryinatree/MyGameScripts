using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public static class Targeting
{
    static List<GameObject> targets;
    public static GameObject target;
    public static GameObject focusOnMe;

    private static int targetNum = 0;

    private static List<GameObject> FindTarget(List<string> tags)
    {


        List<GameObject> enemies = new List<GameObject>();
        foreach (List<GameObject> items in BattleSingleton.Instance.Combatants)
        {
            foreach (string tag in tags)
            {
                if (items[0].transform.tag == tag)
                {
                    foreach (GameObject item in items)
                    {
                        enemies.Add(item);
                    }
                } 
            }
        }

        return enemies;
    }

    private static List<GameObject> TargetsInRange(List<GameObject> enemies, float range, GameObject attacker)
    {
        List<GameObject> enemiesInRange = new List<GameObject>();
        foreach (GameObject item in enemies)
        {
            if(Vector3.Distance(item.transform.position,attacker.transform.position)< range)
            {
                enemiesInRange.Add(item);
            }
        }
        return enemiesInRange;
    }



    public static void SelectTarget()
    {
            focusOnMe = BattleSingleton.Instance.focusOnMe;
            List<string> tags = new List<string>();
            for (int i = 0; i < BattleSingleton.Instance.Combatants.Count; i++)
            {
                if (BattleSingleton.Instance.Combatants[i][0].tag != focusOnMe.tag)
                {
                    tags.Add(BattleSingleton.Instance.Combatants[i][0].tag);
                }
            }
            targets = Targeting.FindTarget(tags);
            targets = Targeting.TargetsInRange(targets, focusOnMe.GetComponent<CharacterController>().myStats.attackRange, focusOnMe);
        if (targets.Count == 0)
        {
            target = null;
            return;
        }
            if (targets != null)
            {
                if (targets.Count > 0)
                {
                    if (targetNum < targets.Count)
                    {
                        target = targets[targetNum];
                        targetNum++;
                        focusOnMe.transform.LookAt(target.transform);
                    //  BattleSingleton.Instance.cam.m_LookAt = target.transform;
                        BattleCamera.SwitchToTargetGroup();
                        BattleSingleton.Instance.aimCam.enabled = true;
                        var transposer = BattleSingleton.Instance.cam.GetCinemachineComponent<CinemachineTransposer>();
                        transposer.m_FollowOffset = Constants.CameraConstants.Aim;
                    }
                    else
                    {
                        targetNum = 0;
                        target = targets[targetNum];
                        targetNum++;
                        focusOnMe.transform.LookAt(target.transform);
                        //BattleSingleton.Instance.cam.m_LookAt = target.transform;
                        BattleCamera.SwitchToTargetGroup();
                        BattleSingleton.Instance.aimCam.enabled = true;
                        var transposer = BattleSingleton.Instance.cam.GetCinemachineComponent<CinemachineTransposer>();
                        transposer.m_FollowOffset = Constants.CameraConstants.Aim;
                    }
                }
        }
    }

    public static void TargetNext()
    {
        if (targets != null)
        {
            if (targets.Count > 0)
            {
                if (targetNum < 0)
                {
                    targetNum = 0;
                }
                if (targetNum < targets.Count-1)
                {
                    targetNum++;
                    target = targets[targetNum];
                        focusOnMe.transform.LookAt(target.transform);
                    BattleCamera.SwitchToTargetGroup();
                }
                else
                {
                    targetNum = 0;
                    target = targets[targetNum];
                    focusOnMe.transform.LookAt(target.transform);
                    BattleCamera.SwitchToTargetGroup();

                }
            }
        }
    }
    public static void TargetPrevious()
    {
        if (targets != null)
        {
            if (targets.Count > 0)
            {
                if (targetNum == targets.Count)
                {
                    targetNum = targets.Count;
                }
                if (targetNum > 0)
                {
                    targetNum--;
                    target = targets[targetNum];
                    focusOnMe.transform.LookAt(target.transform);
                    BattleCamera.SwitchToTargetGroup();
                }
                else
                {
                    targetNum = targets.Count-1;
                    target = targets[targetNum];
                    focusOnMe.transform.LookAt(target.transform);
                    BattleCamera.SwitchToTargetGroup();

                }
            }
        }
    }


}
