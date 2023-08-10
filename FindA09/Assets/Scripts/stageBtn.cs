using UnityEngine;

public class StageBtn : MonoBehaviour
{
    public GameObject StageSelect;
    public AudioClip stageSelectBtnClip;
    public AudioSource stageAudioSource;

    public void StageSetActive()
    {
        stageAudioSource.PlayOneShot(stageSelectBtnClip);
        StageSelect.SetActive(!StageSelect.activeSelf);
    }

}
