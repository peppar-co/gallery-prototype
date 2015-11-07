using UnityEngine;
using System.Collections;

public class SphereHullObject : MonoBehaviour
{

    [SerializeField]
    private bool _gotHit = false;

    public bool GotHit
    {
        get
        {
            return _rigidBody.useGravity;
        }
        set
        {
            _gotHit = value;
            _rigidBody.useGravity = _gotHit;
            if (_gotHit == true)
            {
                _color = Color.red;
            }
            else
            {
                _color = Color.white;
                ResetObject();
            }
        }
    }

    private Rigidbody _rigidBody;
    private Color _color;

    private Transform _defaultTransform;
    private Transform _oldParent;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _defaultTransform = transform;
        _oldParent = transform.parent;
        Debug.Log(_defaultTransform);
        ResetObject();
    }

    private void OnInteraction()
    {
        transform.SetParent(transform.parent.parent);
        GotHit = !GotHit;
        //_rigidBody.AddExplosionForce(1000f, new Vector3(0, 1f, 0), 500f);        


        _rigidBody.AddForce(transform.up * -1000);
        GetComponent<Renderer>().material.color = _color;
    }
    public void OnMouseDown()
    {
        OnInteraction();
    }

    public void ResetObject()
    {
        GotHit = false;
        _color = Color.white;


        Debug.Log("set transform default");

        transform.SetParent(_defaultTransform.parent);
        transform.localPosition = _defaultTransform.localPosition;
        transform.localRotation = _defaultTransform.localRotation;
    }

}
