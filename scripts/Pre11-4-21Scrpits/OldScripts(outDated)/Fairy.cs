using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fairy : MonoBehaviour
{

    public AudioClip[] greetings = null;
    public AudioSource myVoice = null;

    bool talkingToMe = false;

    public Canvas theCanvas;
    protected static GameObject InteractInfo = null;
    CanvasGroup InteractInfoAlpha = null;
    protected static GameObject QuestInfo = null;
    CanvasGroup QuestInfoAlpha = null;

    int voiceNavigator = 0;
    public int questNumber = 1;

    private void Start()
    {
        InteractInfo = theCanvas.transform.GetChild(0).gameObject;
        InteractInfoAlpha = InteractInfo.GetComponent<CanvasGroup>();
        QuestInfo = theCanvas.transform.GetChild(1).gameObject;
        QuestInfoAlpha = QuestInfo.GetComponent<CanvasGroup>();
        Time.timeScale = 1.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myVoice.clip = greetings[voiceNavigator];
            myVoice.Play();
            talkingToMe = true;
            InteractInfoAlpha.alpha = 1;
            if (voiceNavigator == questNumber) QuestInfoAlpha.alpha = 1;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            talkingToMe = false;
            InteractInfoAlpha.alpha = 0;
            QuestInfoAlpha.alpha = 0;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            voiceNavigator++;
            if (voiceNavigator >= greetings.Length)
            {
                voiceNavigator = 0;
            }
            myVoice.clip = greetings[voiceNavigator];
            myVoice.Play();
            Debug.Log(voiceNavigator + " " + questNumber);

            if (voiceNavigator == questNumber)
            {
                QuestInfoAlpha.alpha = 1;
            }
            else
            {
                QuestInfoAlpha.alpha = 0;
            }

            if (greetings.Length == voiceNavigator)
            {
                voiceNavigator = 1;
            }
 
            if(voiceNavigator == questNumber + 1)
            {
                Invoke("LoadNextLevel", greetings[voiceNavigator].length + 1);
            }
        }
    }

    void LoadNextLevel()
    {
        badGuyTurnController.turnQueue.Clear();
        CharacterMain.theGoodGuys.Clear();
        CharacterMain.theBadGuys.Clear();
        cube.allTheCubes.Clear();
        cube.ActionCubes.Clear();
        Application.LoadLevel(Application.loadedLevel + 1);
    }
}
