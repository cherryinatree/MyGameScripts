using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    
    void Start()
    {
        gameObject.transform.position = UserInterface.current_cube.transform.position + new Vector3(0f, 2f, 0) - (gameObject.transform.forward * 4);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            gameObject.transform.position = UserInterface.current_cube.transform.position + new Vector3(0f, 2f, 0) - (gameObject.transform.forward *4);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            gameObject.transform.RotateAround(UserInterface.current_cube.transform.position, new Vector3(0f, 90f, 0f), 90f);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {

            gameObject.transform.RotateAround(UserInterface.current_cube.transform.position, new Vector3(0f, -90f, 0f), 90f);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1)){
            gameObject.transform.Rotate(gameObject.transform.rotation.x + 10, gameObject.transform.rotation.y, gameObject.transform.rotation.z);
        }
    }
}
