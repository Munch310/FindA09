using UnityEngine;

public class StartBGM : MonoBehaviour
{
    public AudioClip startSceneBGM;
    public AudioSource audioSource; // audioSource ���� �߰�

    private void Start()
    {
        audioSource.clip = startSceneBGM;
        audioSource.loop = true;
        audioSource.Play();
    }
}