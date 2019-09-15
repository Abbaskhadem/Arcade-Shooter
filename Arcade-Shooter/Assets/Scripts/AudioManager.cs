using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager _Instance;
    public AudioClip[] Clips;
    // Start is called before the first frame update
    private void Awake()
    {
        _Instance = this;
    }

    public void PlayAudio(int a)
    {
        GetComponent<AudioSource>().clip = Clips[a];
        GetComponent<AudioSource>().Play();
    }
}
