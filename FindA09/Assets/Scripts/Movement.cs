using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float _maxDistance;

    private float   _distance           = 0.0f;
    private float   _angle              = 0.0f;
    private float   _angleDirection     = 1.0f;
    private bool    _isArrieve          = false;

    public bool isArrival { get { return _isArrieve; } }

    private void Start()
    {

        _distance = _maxDistance;

        _angle          = Random.Range(0.0f, 360.0f);
        _angleDirection = (Random.Range(0, 2) % 2 == 0) ? 1.0f : -1.0f;

        InvokeRepeating("Move", Random.Range(0.0f, 1.0f), 1.0f / 30.0f);
        
    }

    private void Update()
    {

        transform.localPosition = new Vector3(_distance * Mathf.Cos(_angle * Mathf.Deg2Rad), _distance * Mathf.Sin(_angle * Mathf.Deg2Rad), 0.0f);

    }

    private void Move()
    {

        if (!_isArrieve)
        {

            _distance    = _distance * 0.95f;
            _angle      -= _angleDirection * 10;

            if(_distance <= 5.0f)
            {

                transform.localPosition = Vector3.zero;
                _distance   = 0.0f;
                _angle      = 0.0f;
                _isArrieve  = true;

                GameManager.instance.ReadyCard();

            }

        }

    }

}
