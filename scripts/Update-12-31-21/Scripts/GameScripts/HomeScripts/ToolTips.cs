using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTips : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        HideToolTip();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void HideToolTip()
    {
        gameObject.SetActive(false);
    }
    public void ShowToolTip()
    {
        gameObject.SetActive(true);
        if (Input.mousePosition.x > 300)
        {
            gameObject.transform.position = new Vector3(Input.mousePosition.x - 300, Input.mousePosition.y + 50, Input.mousePosition.z);
        }else
        {

            gameObject.transform.position = new Vector3(Input.mousePosition.x + 400, Input.mousePosition.y - 50, Input.mousePosition.z);
        }
    }
}
