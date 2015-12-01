using UnityEngine;
using System.Collections;

public class MoveEyeBall : MonoBehaviour
{
    [SerializeField]
    private float _risingSpeed = 1;

    [SerializeField]
    private Transform
        _firstStopPosition,
        _secondStopPosition,
        _endStopPosition;

    [SerializeField]
    private float
        _firstSpeed,
        _secondSpeed,
        _thirdSpeed;

    [SerializeField]
    private float
        _firstWaitingTime,
        _secondWaitingTime;

    [SerializeField]
    private bool _isEyeBallRising;

    private int _currentState = 2;

    private float _lastWaitingStartTime;

    // Use this for initialization
    private void Start()
    {
        _risingSpeed = _firstSpeed;
        _isEyeBallRising = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_isEyeBallRising)
        {
            CheckForStopEyeBallRising();
        }
        else
        {
            CheckForStartEyeBallRising();
        }
    }

    private void FixedUpdate()
    {
        if (_isEyeBallRising)
            EyeballRising();
    }

    //public void StartEyeBallRising()
    //{
    //    _isEyeBallRising = true;
    //}

    //public void StartEyeBallRising(float speed)
    //{
    //    _risingSpeed = speed;

    //    StartEyeBallRising();
    //}

    private void EyeballRising()
    {
        transform.position += Vector3.up * _risingSpeed / 100;
    }

    private void CheckForStopEyeBallRising()
    {
        switch(_currentState)
        {
            case 0:
                if(transform.position.y > _endStopPosition.position.y)
                {
                    _isEyeBallRising = false;
                }
                break;
            case 1:
                if(transform.position.y > _secondStopPosition.position.y)
                {
                    _isEyeBallRising = false;
                    _lastWaitingStartTime = Time.time;
                }
                break;
            case 2:
                if(transform.position.y > _firstStopPosition.position.y)
                {
                    _isEyeBallRising = false;
                    _lastWaitingStartTime = Time.time;
                }
                break;
        }
    }

    private void CheckForStartEyeBallRising()
    {
        switch (_currentState)
        {
            case 0:
                break;
            case 1:
                if (Time.time >= _lastWaitingStartTime + _secondWaitingTime
                    && transform.position.y > _secondStopPosition.position.y)
                {
                    _risingSpeed = _thirdSpeed;
                    _currentState = 0;
                    _isEyeBallRising = true;
                }
                break;
            case 2:
                if (Time.time >= _lastWaitingStartTime + _firstWaitingTime
                    && transform.position.y > _firstStopPosition.position.y)
                {
                    _risingSpeed = _secondSpeed;
                    _currentState = 1;
                    _isEyeBallRising = true;
                }
                break;
        }
    }
}
