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

    public CardScriptable _cardScriptable    = null;
    public StageData      _stageData       = null;
    
    private bool[] isClearStage = { false, false, false };


    public GameObject _gameOverUI;


    public CardScriptable   cardScriptable { get { return _cardScriptable; } }
    public StageData        stageData { get { return _stageData; } }

    public int maxCurrentStageCardNumber { get { return _stageData.array[Global.Instance.CurrentStage].cardNumber; } }
    public int cardIndexNumber { get { return maxCurrentStageCardNumber / 2; } }            //카드 인덱스의 종류

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

        Debug.Log(Global.Instance.CurrentStage);

        LoadPlayerPrefs();
        DontDestroyOnLoad(gameObject);

        LoadStage();

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

        //값들이 유효한지 검사합니다.
        Debug.Assert(cardIndexNumber <= _cardScriptable.array.Length);       //카드의 종류가 최대치를 넘어서선 안됩니다.

        int[] randData = new int[maxCurrentStageCardNumber];
        for(int i =0; i < cardIndexNumber; ++i)
        {
            randData[i * 2 + 0] = i;
            randData[i * 2 + 1] = i;
        }
        randData = randData.OrderBy(items => Random.Range(-1.0f, 1.0f)).ToArray();

        for (int i = 0; i < maxCurrentStageCardNumber; i++)
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

    public void LoadStage()
    {
        ResetStage();
        switch (Global.Instance.CurrentStage)
        {
            case 0:
                _stage1.SetActive(true);
                _cardParentTransform = _stage1.transform;
                break;
            case 1:
                _stage2.SetActive(true);
                _cardParentTransform = _stage2.transform;
                break;
            case 2:
                _stage3.SetActive(true);
                _cardParentTransform = _stage3.transform;
                break;
            default:
                Debug.Assert(false);
                break;
        }
        CreateCard(Global.Instance.CurrentStage);
    }

    public void SelectStage(int stageLevel)
    {
        Global.Instance.CurrentStage = stageLevel;
        SceneManager.LoadScene("MainScene");
    }

    public void NextStage()
    {
        Global.Instance.CurrentStage = Mathf.Max(0, Mathf.Min(++Global.Instance.CurrentStage, _stageData.maxStage));
        LoadStage();
    }

    public void IsMatched()
    {

    }

    public bool IsAvailableCardIndex(int cardIndex)
    {
        return cardIndex != Card.INVALID_CARD_INDEX && cardIndex < cardIndexNumber;
    }

}
