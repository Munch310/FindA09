using UnityEngine;

public class ReturnMainBtn : MonoBehaviour
{
    public GameObject StageSelect;

    public void StageSetActive()
    {
        StageSelect.SetActive(!StageSelect.active);
    }
}
