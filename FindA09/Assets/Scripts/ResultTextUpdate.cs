using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultTextUpdate : MonoBehaviour
{

    public Text timesLeftText   = null;
    public Text totalTriesText  = null;
    public Text totalScoreText  = null;
    public Text bestScoreText   = null;


    public void UpdateText()
    {

        timesLeftText.text  = $"남은 시간: {GameManager.instance.time}";
        totalTriesText.text = $"시도 횟수: {GameManager.instance.tryCount}";
        totalScoreText.text = $"총 점수: {GameManager.instance.score}";
        bestScoreText.text  = $"0000";

    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

}
