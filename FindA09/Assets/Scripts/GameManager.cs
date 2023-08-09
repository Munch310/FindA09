using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    public static GameManager instance { get { return _instance; } }


    private GameObject _firstCard;

    public Text                 _timeText                       = null;
    public Animator             _timeTextAnimator               = null;
    public Text                 _countText                      = null;
    public TextMessage[]        _matchingSuccessTextArray       = new TextMessage[3];
    public TextMessage          _matchingFailureText            = null;
    public GameObject           _gameClearUI                    = null;
    public GameObject           _gameFailureUI                  = null;
    private float               _time                           = 60.0f;
    private int                 _tryCount                       = 0;
    private int                 _matchingCount                  = 0;

    public  GameObject   _cardPrefab             = null;
    private Transform    _cardParentTransform    = null;

    public GameObject _stage1   = null;
    public GameObject _stage2   = null;
    public GameObject _stage3   = null;

    public CardScriptable _cardScriptable       = null;
    public StageData      _stageData            = null;
    

    public float time { get { return _time; } }

    public int score { get { return (int)Mathf.Ceil(_time) * 100 + _matchingCount * 200 + Mathf.Min(maxRankTryCount - tryCount, 0) * -50; } }

    public int tryCount { get { return _tryCount; } }

    public int maxRankTryCount { get { return _tryCount * 3; } }

    public CardScriptable   cardScriptable { get { return _cardScriptable; } }
    public StageData        stageData { get { return _stageData; } }

    public StageData.STAGE_DATA currentStageData { get { return _stageData.array[Global.Instance.CurrentStage]; } }

    public int maxCurrentStageCardNumber { get { return currentStageData.cardNumber; } }

    public float maxCurrentStageTime { get { return currentStageData.time; } }

    public float currentStageHurryUpTime { get { return maxCurrentStageTime * 0.5f; } }

    public int cardIndexNumber { get { return maxCurrentStageCardNumber / 2; } }            //카드 인덱스의 종류

    public void NextStage()
    {
        Global.Instance.CurrentStage = Mathf.Max(0, Mathf.Min(++Global.Instance.CurrentStage, _stageData.maxStage));
        LoadStage();
    }

    public bool IsAvailableCardIndex(int cardIndex)
    {
        return cardIndex != Card.INVALID_CARD_INDEX && cardIndex < cardIndexNumber;
    }

    public void AddOpenCard(GameObject gameObject)
    {

        Debug.Assert(gameObject != null);

        //이전에 기억한 카드와 동일한 카드면 무시합니다.
        if (gameObject == _firstCard)
        {
            return;
        }

        //이전에 입력한 카드가 없으면 기억합니다.
        if (_firstCard == null)
        {
            _firstCard = gameObject;
        }
        else
        {

            Card firstCardScript    = _firstCard.GetComponent<Card>();
            Card secondCardScript   = gameObject.GetComponent<Card>();
            Debug.Assert(firstCardScript != null);
            Debug.Assert(secondCardScript != null);

            //기억한 카드가 유효한 상태인지 검사합니다.
            //유효한 상태
            //1. 카드가 오픈 상태입니다.
            //2. 카드의 상태가 변하는중(애니메이션 진행중)이 아니어야 합니다.
            if (firstCardScript.isOpen && !firstCardScript.isFlipAnimation)
            {

                TextMessage textMessage = null;
                if (firstCardScript.cardIndex == secondCardScript.cardIndex)
                {

                    firstCardScript.OnMatching();
                    secondCardScript.OnMatching();

                    ++_matchingCount;
                    if (_matchingCount == cardIndexNumber)
                    {

                        Time.timeScale = 0.0f;

                        _gameClearUI.SetActive(true);
                        var resultTextUpdate = _gameClearUI.GetComponent<ResultTextUpdate>();
                        Debug.Assert(resultTextUpdate != null);
                        resultTextUpdate.UpdateText();

                    }

                    textMessage = _matchingSuccessTextArray[firstCardScript.cardIndex % 3];

                }
                else
                {
                    
                    firstCardScript.Flip();
                    secondCardScript.Flip();

                    textMessage = _matchingFailureText;

                }

                ++_tryCount;
                _firstCard = null;

                textMessage.OnText();

            }
            else    //유효하지 않다면 기억된 카드를 버리고 새로 입력된 카드를 기억합니다.
            {
                _firstCard = gameObject;
            }

        }

    }

    private void Awake()
    {
        _instance = this;
        LoadStage();
    }

    private void Start()
    {
        Time.timeScale = 1.0f;
    }

    private void Update()
    {
        _time -= Time.deltaTime;
        _timeText.text = $"{_time:N2}";
        _countText.text = $"Count : {_tryCount}";
        
        if (_time <= 0)
        {
            _time = 0;
            Time.timeScale = 0.0f;
            
            // 게임 오버
            _gameFailureUI.SetActive(true);
            var resultTextUpdate = _gameFailureUI.GetComponent<ResultTextUpdate>();
            Debug.Assert(resultTextUpdate);
            resultTextUpdate.UpdateText();

        }

        if (_time <= currentStageHurryUpTime)
        {
            _timeTextAnimator.SetBool("On", true);
        }

    }

    private void CreateCard(int stageLevel)
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

            Card cardScript = newCard.GetComponent<Card>();
            Debug.Assert(cardScript != null);
            cardScript.cardIndex = randData[i];

        }

    }

    private void ResetStage()
    {

        _time           = 0;
        _tryCount          = 0;
        _matchingCount  = 0;
        
        _stage1.SetActive(false);
        _stage2.SetActive(false);
        _stage3.SetActive(false);

        Time.timeScale = 1.0f;

    }

    private void LoadStage()
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

        _time = _stageData.array[Global.Instance.CurrentStage].time;

    }

}
