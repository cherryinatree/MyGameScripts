using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class villager : MonoBehaviour
{
    public AudioClip[] greetings = null;
    public AudioSource myVoice = null;

    bool talkingToMe = false;

    int voiceNavigator = 0;
    public int questNumber = 1;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myVoice.clip = greetings[voiceNavigator];
            myVoice.Play();
            talkingToMe = true;

        }
    }
}
