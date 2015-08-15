using UnityEngine;
using System.Collections;

public class HitStuff : MonoBehaviour
{

    [SerializeField]
    public bool _gotHit;

    public bool GotHit
    {
        get { return _gotHit; }
        set { _gotHit = value; }
    }

    [SerializeField]
    Color _highlightColor;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_gotHit)
        {
            gameObject.GetComponent<Renderer>().material.color = _highlightColor;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
