using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryBtn : MonoBehaviour
{
    public void GotoMain()
    {
        SceneManager.LoadScene("StartScene");
    }
}
