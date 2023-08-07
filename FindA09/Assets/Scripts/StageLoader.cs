using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageLoader : MonoBehaviour
{
    public Text _timeText;
    public Text _countText;

    public GameObject _stage1;
    public GameObject _stage2;
    public GameObject _stage3;
    public GameObject _gameFinishUI;

    private void Awake() 
    {
        GameManager.Instance()._timeText = _timeText;
        GameManager.Instance()._countText = _countText;
        GameManager.Instance()._stage1 = _stage1;
        GameManager.Instance()._stage2 = _stage2;
        GameManager.Instance()._stage3 = _stage3;
        GameManager.Instance()._gameOverUI = _gameFinishUI;
    }

    void Start()
    {
        GameManager.Instance().LoadStage(GameManager.Instance()._currentStage);
        
    }
}
