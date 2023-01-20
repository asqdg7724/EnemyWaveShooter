using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    AudioSource myAudio;

    public AudioClip[] sounds;

    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    public void SoundPlay(int soundNum)
    {
        myAudio.clip = sounds[soundNum];

        myAudio.Play();
    }
}
