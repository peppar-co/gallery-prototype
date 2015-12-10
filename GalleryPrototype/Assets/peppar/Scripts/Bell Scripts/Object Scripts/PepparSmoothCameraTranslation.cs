using UnityEngine;
using System.Collections;

public class PepparSmoothCameraTranslation : MonoBehaviour
{

    [SerializeField]
    private Transform _trackingCamera;

    [SerializeField]
    private bool _useLerp;

    [SerializeField, Range(0.01f, 1f)]
    private float _lerpSpeed;

    private Vector3 _currentPosition;
    private Vector3 _newPosition;

    private Quaternion _currentRotation;
    private Quaternion _newRotation;

    private void FixedUpdate()
    {
        if (_useLerp)
        {
            _currentPosition = transform.position;
            _newPosition = _trackingCamera.position;
            _currentRotation = transform.rotation;
            _newRotation = _trackingCamera.rotation;
            //if (_currentPosition != _newPosition || _currentRotation != _newRotation)
            //{



                transform.position = Vector3.Lerp(_currentPosition, _newPosition, _lerpSpeed);
                transform.rotation = Quaternion.Lerp(_currentRotation, _newRotation, _lerpSpeed);


            //    Debug.Log(_currentPosition + "::_currentPosition::" + _newPosition + "::_newPosition::");
            //}






        }
        else
        {
            transform.position = _trackingCamera.position;
            transform.rotation = _trackingCamera.rotation;
        }
    }


}
