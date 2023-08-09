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
        // ���� Ŭ������ �������� ��������
        int currentClearedStage = Global.Instance.CurrentStage;

        if (currentClearedStage >= stage)
        {
            // ���� Ŭ������ ���������� ������ ������������ ũ�ų� ������ ����
            Global.Instance.CurrentStage = stage;
            SceneManager.LoadScene("MainScene");
        }
        else
        {
            // ���� Ŭ������ ������������ �� ���� ���������� ������ ���, �������� ����
            Debug.Log("Ŭ����X");
        }
    }


}

