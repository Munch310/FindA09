using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public GameObject _firstCard;
    public GameObject _secondCard;

    public Text _timeText;
    public Text _countText;
    public float _time = 60.0f;
    public int _score = 0;
    public int _count = 0;

    public GameObject _cardPrefab;
    public Transform _cardParentTransform;

    public GameObject _stage1;
    public GameObject _stage2;
    public GameObject _stage3;
    public int _currentStage = 1;

    public CardScriptable[] _cardScriptableArray = null;
    
    int _maxCardCount = 12;

    private bool[] isClearStage = { false, false, false };


    public GameObject _gameOverUI;


    public CardScriptable[] cardScriptableArray { get { return _cardScriptableArray; } }

    public int cardIndexNumber { get { return _maxCardCount / 2; } }            //카드 인덱스의 종류

    public static GameManager Instance()
    {
        if (!_instance)
        {
            _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

            if (_instance == null)
                Debug.Log("No Singleton Objs");

        }
        return _instance;
    }

    private void Awake()
    {
        if (!_instance)
            _instance = this;

        if (_instance != this)
            Destroy(gameObject);

        LoadPlayerPrefs();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Time.timeScale = 1.0f;
    }

    private void Update()
    {
        _time -= Time.deltaTime;
        _timeText.text = _time.ToString("N2");
        _countText.text = $"Count : {_count}";
        if (_time <= 0)
        {
            _time = 0;
            Time.timeScale = 0.0f;
            // 게임 오버
            _gameOverUI.SetActive(true);
        }
    }

    void LoadPlayerPrefs()
    {
        for (int i = 1; i < 4; i++)
        {
            if (PlayerPrefs.HasKey($"Stage{i}Clear"))
            {
                isClearStage[i - 1] = true;
            }
        }
    }

    public void CreateCard(int stageLevel)
    {
        if (_cardParentTransform.childCount != 0)
        {
            for (int i = 0; i < _cardParentTransform.childCount; i++)
                Destroy(_cardParentTransform.GetChild(i).gameObject);
        }

        int[] randData = { };
        switch (stageLevel)
        {
            case 1:
                _maxCardCount = 12;
                int[] randData_1 = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5 };
                randData = randData_1;
                break;

            case 2:
                _maxCardCount = 16;
                int[] randData_2 = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
                randData = randData_2;
                break;

            case 3:
                _maxCardCount = 20;
                int[] randData_3 = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9 };
                randData = randData_3;
                break;
        }

        //값들이 유효한지 검사합니다.
        Debug.Assert(_maxCardCount % 2 == 0);                               //짝이 맞아야 합니다.
        Debug.Assert(_maxCardCount == randData.Length);                     //배열의 크기가 충분해야 합니다.
        Debug.Assert(cardIndexNumber <= _cardScriptableArray.Length);       //카드의 종류가 최대치를 넘어서선 안됩니다.

        randData = randData.OrderBy(items => Random.Range(-1.0f, 1.0f)).ToArray();
        for (int i = 0; i < _maxCardCount; i++)
        {
            GameObject newCard = Instantiate(_cardPrefab);
            newCard.transform.SetParent(_cardParentTransform);
            newCard.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void ResetStage()
    {
        _time = 60;
        Time.timeScale = 1.0f;
        _stage1.SetActive(false);
        _stage2.SetActive(false);
        _stage3.SetActive(false);
    }

    public void LoadStage(int stageLevel)
    {
        ResetStage();
        _currentStage = stageLevel;
        switch (stageLevel)
        {
            case 1:
                _stage1.SetActive(true);
                _cardParentTransform = _stage1.transform;
                break;
            case 2:
                _stage2.SetActive(true);
                _cardParentTransform = _stage2.transform;
                break;
            case 3:
                _stage3.SetActive(true);
                _cardParentTransform = _stage3.transform;
                break;
        }
        CreateCard(stageLevel);
    }

    public void SelectStage(int stageLevel)
    {
        _currentStage = stageLevel;
        SceneManager.LoadScene("MainScene");
    }

    public void NextStage()
    {
        _currentStage++;
        if (_currentStage == 3)
            _currentStage = 3;
        LoadStage(_currentStage);
    }

    public void IsMatched()
    {

    }

    public bool IsAvailableCardIndex(int cardIndex)
    {
        return cardIndex != Card.INVALID_CARD_INDEX && cardIndex < cardIndexNumber;
    }

}
