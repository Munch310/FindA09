using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleBtn : MonoBehaviour
{
    public void GotoMain()
    {
        SceneManager.LoadScene("StartScene");
    }
}
