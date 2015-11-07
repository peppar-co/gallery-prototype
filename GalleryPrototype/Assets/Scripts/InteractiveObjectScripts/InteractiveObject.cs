using UnityEngine;
using System.Collections;

public class InteractiveObject : MonoBehaviour
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
            }
        }
    }

    private Rigidbody _rigidBody;
    private Color _color;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        GotHit = false;
        _color = Color.white;
    }

    private void OnMouseDown()
    {
        transform.SetParent(transform.parent.parent);

        GotHit = !GotHit;
        //_rigidBody.AddExplosionForce(1000f, new Vector3(0, 1f, 0), 500f);
        _rigidBody.AddForce(transform.up * -1000);
        GetComponent<Renderer>().material.color = _color;
    }

    public void OnInteraction()
    {
        OnMouseDown();
    }
}
