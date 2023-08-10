using UnityEngine;

public class StageBtn : MonoBehaviour
{
    public GameObject StageSelect;
    public AudioClip stageSlectBtnClip;
    public AudioSource audioSource;

    public void StageSetActive()
    {
        audioSource.PlayOneShot(stageSlectBtnClip);
        StageSelect.SetActive(!StageSelect.activeSelf);
    }
}