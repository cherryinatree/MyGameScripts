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
        foreach (string item in tags)
        {
            GameObject[] enemy = GameObject.FindGameObjectsWithTag(item);

            foreach (GameObject one in enemy)
            {
                enemies.Add(one);
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



    public static void SelectTarget(List<List<GameObject>> Combatants, GameObject focus, CinemachineVirtualCamera cam, Cinemachine3rdPersonAim aimCam)
    {
            focusOnMe = focus;
            List<string> tags = new List<string>();
            for (int i = 0; i < Combatants.Count; i++)
            {
                if (Combatants[i][0].tag != focusOnMe.tag)
                {
                    tags.Add(Combatants[i][0].tag);
                }
            }
            targets = Targeting.FindTarget(tags);
            targets = Targeting.TargetsInRange(targets, focusOnMe.GetComponent<CharacterController>().myStats.attackRange, focusOnMe);
            if (targets != null)
            {
                if (targets.Count > 0)
                {
                    if (targetNum < targets.Count)
                    {
                        target = targets[targetNum];
                        targetNum++;
                        focusOnMe.transform.LookAt(target.transform);
                        cam.m_LookAt = target.transform;
                        aimCam.enabled = true;
                        var transposer = cam.GetCinemachineComponent<CinemachineTransposer>();
                        transposer.m_FollowOffset = Constants.CameraConstants.Aim;
                    }
                    else
                    {
                        targetNum = 0;
                        target = targets[targetNum];
                        targetNum++;
                        focusOnMe.transform.LookAt(target.transform);
                        cam.m_LookAt = target.transform;
                        aimCam.enabled = true;
                        var transposer = cam.GetCinemachineComponent<CinemachineTransposer>();
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
                }
                else
                {
                    targetNum = 0;
                    target = targets[targetNum];
                    focusOnMe.transform.LookAt(target.transform);

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
                }
                else
                {
                    targetNum = targets.Count-1;
                    target = targets[targetNum];
                    focusOnMe.transform.LookAt(target.transform);

                }
            }
        }
    }

}
