using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{

    public const int    INVALID_CARD_INDEX = -1;

    public const float  CARD_OPEN_DURATION = 5.0f;


    public int      _cardIndex = Card.INVALID_CARD_INDEX;            //카드의 번호입니다. 이 값으로 카드를 구분, 각종 정보를 얻는 키값으로 사용합니다.


    public GameObject  _front          = null;
    public Image       _frontImage     = null;
    public GameObject  _back           = null;
    public Image       _backImage      = null;
    public Animator    _animator       = null;

    private bool    _isOpen             = false;
    private bool    _isFlipAnimation    = false;
    private float   _openTime           = 0.0f;
    private bool    _isMatching         = false;


    public int cardIndex { set { _cardIndex = value; UpdateCardIndex(); } get { return _cardIndex; } }

    public bool isOpen { get { return _isOpen; } }

    public bool isFlipAnimation { get { return _isFlipAnimation; } }


    public void Flip()
    {
        _animator.SetBool("IsOpen", !_animator.GetBool("IsOpen"));
    }

    public void OnClick()
    {
        if (GameManager.instance.isStageReady && !_isFlipAnimation)
        {
            Flip();
        }
    }

    public void OnFlipAnimationStart()
    {
        _isFlipAnimation = true;
    }

    public void OnFlipAnimationEnd()
    {

        _isFlipAnimation = false;
        _isOpen = !_isOpen;

        //오픈 되었으면 오픈된 시간을 기록합니다. 일정 시간 후 다시 뒤집히게 하는데 사용됩니다.
        if (isOpen)
        {

            _openTime = Time.time;

            //한번 오픈이 되었으면 뒷면에 변화를 줍니다.
            var backImage = _backImage.GetComponent<Image>();
            backImage.color = Color.gray;

            GameManager.instance.AddOpenCard(gameObject);

        }

    }

    public void OnMatching()
    {
        _frontImage.gameObject.SetActive(false);
        _backImage.gameObject.SetActive(false);
        _isMatching = true;
    }

    private void Awake()
    {

        Debug.Assert(_front != null);
        Debug.Assert(_frontImage != null);


        Debug.Assert(_back != null);
        Debug.Assert(_backImage != null);

    }

    private void Start()
    {
    }

    private void Update()
    {

        //적합한 상태일 때를 제외한 나머지 상황에서 오픈된 카드를 다시 닫을지 살펴봅니다.
        //적합한 상태:
        //1.오픈 상태
        //2.뒤집히는 중이 아닐 때(뒤집힐 때에는 무시합니다)
        if (!_isMatching && isOpen && !isFlipAnimation)
        {

            //카드가 오픈된지 일정시간이 경과 했으면 다시 뒤집습니다.
            if(Time.time -  _openTime > CARD_OPEN_DURATION)
            {
                Flip();
            }

        }
        
    }


    private void UpdateCardIndex()
    {

        Debug.Assert(GameManager.instance.IsAvailableCardIndex(_cardIndex));

        var frontImage      = _frontImage.GetComponent<Image>();
        frontImage.sprite   = GameManager.instance.cardScriptable.array[_cardIndex]._sprite;
        Debug.Assert(frontImage.sprite != null);
        
    }

}
