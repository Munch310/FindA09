using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBtn : MonoBehaviour
{
    public void GamseStart()
    {
        SceneManager.LoadScene("MainScene");
    }
}
