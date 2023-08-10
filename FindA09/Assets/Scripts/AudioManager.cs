using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip bgmusic;
    public AudioSource audioSource;

    void Start()
    {
        //계속 음악 실행
        audioSource.clip = bgmusic;
        audioSource.loop = true;
        audioSource.Play();
    }
}