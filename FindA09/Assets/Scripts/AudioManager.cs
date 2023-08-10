using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip mainSceneBGM;
    public AudioSource mainAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        mainAudioSource.clip = mainSceneBGM;
        mainAudioSource.loop = true;
        mainAudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
