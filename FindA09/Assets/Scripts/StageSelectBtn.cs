using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelectBtn : MonoBehaviour
{
    public Button[] stageButtons; // 스테이지 버튼 배열
    int currentClearedStage = Global.Instance.CurrentStage;

    void Start()
    {
        // 설정하실 때, 각 StageSelectBtn1~3까지 Script 넣어주시고
        // StageSelectBtn(Script)에서 Stage Button 3, Element0~2까지 각 버튼 다 넣어주시면됩니다!
        // 각 스테이지 버튼의 상태 설정
        for (int i = 0; i < stageButtons.Length; i++)
        {
            // i + 1이 클리어한 스테이지보다 작거나 같으면 잠금 해제
            stageButtons[i].interactable = i + 1 <= currentClearedStage + 1;
        }

        // 마지막 스테이지 이후의 버튼 비활성화
        for (int i = currentClearedStage + 1; i < stageButtons.Length; i++)
        {
            stageButtons[i].interactable = false;
        }
    }

    public void SetStage(int stage)
    {
        // 현재 클리어한 스테이지 다음 스테이지를 선택한 경우
        if (currentClearedStage + 1 >= stage)
        {
            Global.Instance.CurrentStage = stage;
            SceneManager.LoadScene("MainScene");
        }
    }
}
