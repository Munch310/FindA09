using UnityEngine;

public class StageBtn : MonoBehaviour
{
    public GameObject StageSelect;

    public void StageSetActive()
    {
        StageSelect.SetActive(!StageSelect.active);
    }

}
