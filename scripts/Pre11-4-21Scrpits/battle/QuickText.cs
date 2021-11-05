using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickText : MonoBehaviour
{
    public Text MyText = null;
    public float deathTime = 4f;
    Vector2 screenPosition;
    float upDownX = -1;
    float upDownY = 0;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("death", deathTime);
    }

    private void FixedUpdate()
    {
        screenPosition = transform.position;
        upDownX += 0.02f;
        upDownY = upDownX * upDownX;
        screenPosition.y = screenPosition.y + upDownY*4;
        transform.position = screenPosition;
    }


    public void QuickTextSays(string texts)
    {
        MyText.text = texts;
    }

    void death()
    {
        Destroy(gameObject);
    }
}
