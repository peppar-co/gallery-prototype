using UnityEngine;
using System.Collections;

public class IdleObject : MonoBehaviour {


    [SerializeField]
    private Vector3 _rotateAroundAxis;
    [SerializeField]
    private float _speed;


    private void Update()
    {
        transform.Rotate(_rotateAroundAxis, _speed);
    }


}
