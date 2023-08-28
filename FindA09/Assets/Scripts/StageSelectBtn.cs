using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelectBtn : MonoBehaviour
{
    public Button[] stageButtons; // �������� ��ư �迭
    int currentClearedStage = Global.Instance.CurrentStage;

    void Start()
    {
        // �����Ͻ� ��, �� StageSelectBtn1~3���� Script �־��ֽð�
        // StageSelectBtn(Script)���� Stage Button 3, Element0~2���� �� ��ư �� �־��ֽø�˴ϴ�!
        // �� �������� ��ư�� ���� ����
        for (int i = 0; i < stageButtons.Length; i++)
        {
            // i + 1�� Ŭ������ ������������ �۰ų� ������ ��� ����
            stageButtons[i].interactable = i + 1 <= currentClearedStage + 1;
        }

        // ������ �������� ������ ��ư ��Ȱ��ȭ
        for (int i = currentClearedStage + 1; i < stageButtons.Length; i++)
        {
            stageButtons[i].interactable = false;
        }
    }

    public void SetStage(int stage)
    {
        // ���� Ŭ������ �������� ���� ���������� ������ ���
        if (currentClearedStage + 1 >= stage)
        {
            Global.Instance.CurrentStage = stage;
            SceneManager.LoadScene("MainScene");
        }
    }
}
