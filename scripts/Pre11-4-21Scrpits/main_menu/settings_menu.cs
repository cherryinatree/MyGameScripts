using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settings_menu : MonoBehaviour
{

    public GameObject main = null;
    public GameObject video = null;
    public GameObject controls = null;
    public GameObject sound = null;

   

    public void mainSettings()
    {
        main.SetActive(true);
        video.SetActive(false);
        controls.SetActive(false);
        sound.SetActive(false);
    }

    public void videoSettings()
    {
        main.SetActive(false);
        video.SetActive(true);
        controls.SetActive(false);
        sound.SetActive(false);
    }

    public void controlsSettings()
    {
        main.SetActive(false);
        video.SetActive(false);
        controls.SetActive(true);
        sound.SetActive(false);
    }

    public void soundSettings()
    {
        main.SetActive(false);
        video.SetActive(false);
        controls.SetActive(false);
        sound.SetActive(true);
    }
}
