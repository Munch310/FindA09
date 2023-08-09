using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectBtn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {

    }

    public void SetStage(int stage)
    {
        // 현재 클리어한 스테이지 가져오기
        int currentClearedStage = Global.Instance.CurrentStage;

        if (currentClearedStage >= stage)
        {
            // 현재 클리어한 스테이지가 선택한 스테이지보다 크거나 같으면 실행
            Global.Instance.CurrentStage = stage;
            SceneManager.LoadScene("MainScene");
        }
        else
        {
            // 현재 클리어한 스테이지보다 더 높은 스테이지를 선택한 경우, 실행하지 않음
            Debug.Log("클리어X");
        }
    }


}

