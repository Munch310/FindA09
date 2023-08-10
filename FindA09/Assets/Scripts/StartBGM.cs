using UnityEngine;

public class StartBGM : MonoBehaviour
{
    public AudioClip startSceneBGM;
    public AudioSource audioSource; // audioSource 변수 추가

    private void Start()
    {
        audioSource.clip = startSceneBGM;
        audioSource.loop = true;
        audioSource.Play();
    }
}