using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{

    public const int    INVALID_CARD_INDEX = -1;

    public const float  CARD_OPEN_DURATION = 5.0f;


    public int      _cardIndex = Card.INVALID_CARD_INDEX;            //ī���� ��ȣ�Դϴ�. �� ������ ī�带 ����, ���� ������ ��� Ű������ ����մϴ�.


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

        //���� �Ǿ����� ���µ� �ð��� ����մϴ�. ���� �ð� �� �ٽ� �������� �ϴµ� ���˴ϴ�.
        if (isOpen)
        {

            _openTime = Time.time;

            //�ѹ� ������ �Ǿ����� �޸鿡 ��ȭ�� �ݴϴ�.
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

        //������ ������ ���� ������ ������ ��Ȳ���� ���µ� ī�带 �ٽ� ������ ���캾�ϴ�.
        //������ ����:
        //1.���� ����
        //2.�������� ���� �ƴ� ��(������ ������ �����մϴ�)
        if (!_isMatching && isOpen && !isFlipAnimation)
        {

            //ī�尡 ���µ��� �����ð��� ��� ������ �ٽ� �������ϴ�.
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
