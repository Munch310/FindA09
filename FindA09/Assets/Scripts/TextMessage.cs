using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextMessage : MonoBehaviour
{

    public Text     _text = null;
    public float    _duration = 3.0f;

    private float _onTime = 0.0f;

    public void OnText()
    {

        gameObject.SetActive(true);

        _onTime = Time.time;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        float parameter = Mathf.Min(1.0f, (Time.time - _onTime) / _duration);
        _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, 1.0f - parameter);

        if (parameter >= 1.0f)
        {
            gameObject.SetActive(false);
        }
        
    }
}
