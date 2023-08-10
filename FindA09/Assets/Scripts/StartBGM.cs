using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBGM : MonoBehaviour
{
    public AudioClip startSecneBGM;
    public AudioSource startAudioSource;

    public void Start()
    {
        startAudioSource.clip = startSecneBGM;
        startAudioSource.loop = true;
        startAudioSource.Play();
    }
}
