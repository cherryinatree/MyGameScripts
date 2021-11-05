using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    public void MoveToFrom(GameObject start, GameObject end)
    {
        float speed = 1f;
        float distance = Vector3.Distance(start.transform.position, end.transform.position);
        start.GetComponent<CharacterMain>().my_cube = end;
        start.transform.rotation = Quaternion.Slerp(start.transform.rotation, Quaternion.LookRotation(end.transform.position - start.transform.position), 2 * Time.deltaTime);
        
    }
    
    
}
