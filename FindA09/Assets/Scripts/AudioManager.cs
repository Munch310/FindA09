using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip bgmusic;
    public AudioSource audioSource;

    void Start()
    {
        //��� ���� ����
        audioSource.clip = bgmusic;
        audioSource.loop = true;
        audioSource.Play();
    }
}