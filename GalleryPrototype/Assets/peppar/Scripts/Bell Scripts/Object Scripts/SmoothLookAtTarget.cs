using UnityEngine;
using System.Collections;

public class SmoothLookAtTarget : MonoBehaviour
{
    [SerializeField]
    private Transform _lookAtTarget;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private bool _useDampening;

    private void Awake()
    {
        UnityEngine.Assertions.Assert.IsNotNull(_lookAtTarget);
        // make sure speed is not smaller than 0
        _speed = (_speed <= 0) ? 1 : _speed;
    }

    private void Update()
    {
        if (_useDampening)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_lookAtTarget.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _speed * Time.deltaTime);
        }
        else
        {
            transform.LookAt(_lookAtTarget);
        }
    }
}
