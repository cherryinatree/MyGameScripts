using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamingTools;
using UnityEngine.AI;


public class CharacterController: MonoBehaviour
{
    public Stats myStats;

    void Start()
    {
        myStats = new Stats();
        gameObject.AddComponent<NavMeshAgent>();
        gameObject.GetComponent<NavMeshAgent>().radius = 0.4f;
        gameObject.AddComponent<BoxCollider>();
        gameObject.AddComponent<LineRenderer>();
        gameObject.GetComponent<LineRenderer>().material = ResourseLoader.GetMaterial("Materials/BlueLine");
        gameObject.GetComponent<LineRenderer>().SetPosition(1, new Vector3(0, 0, 0));
    }

    private void Update()
    {
        if(GetComponent<NavMeshAgent>().remainingDistance > 0.3)
        {
            GetComponent<Animator>().SetBool("Moving", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("Moving", false);
        }
    }
}
