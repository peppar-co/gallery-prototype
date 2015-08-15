using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class HighlightObject
{
    [SerializeField]
    private Transform _object;

    [SerializeField]
    private Transform _highlight;

    private bool _highlighted;

    private float _highlightTime = int.MinValue;

    public bool Highlighted
    {
        get { return _highlighted; }
        set
        {
            if(_highlighted != value)
            {
                _highlightTime = Time.time;
                _highlighted = value;
            }
        }
    }

    public float HighlightTime
    {
        get { return _highlightTime; }
    }

    public Transform Object
    {
        get { return _object; }
    }

    public Transform Highlight
    {
        get { return _highlight; }
    }

    public Color StandardColor
    {
        get; set;
    }
}

public class HighlightSystem : MonoBehaviour
{
    //[SerializeField]
    //private LayerMask _layerMask;

    [SerializeField]
    private Color _highlightColor;

    [SerializeField]
    private List<HighlightObject> _highlightObjects;

    [SerializeField]
    private float _minHighlightTime;

    [SerializeField]
    RaycastHit[] _hits;

    private Ray _ray;
    private RaycastHit _hit;

    List<RaycastHit> _bucketList = new List<RaycastHit>();

    private void Awake()
    {
        if(_highlightObjects == null)
        {
            _highlightObjects = new List<HighlightObject>();
        }

        foreach (var highlightObject in _highlightObjects)
        {
            if (highlightObject.Highlight != null)
            {
                var renderer = highlightObject.Object.GetComponent<Renderer>();

                if (renderer != null && renderer.material != null)
                {
                    highlightObject.StandardColor = renderer.material.color;
                }

                highlightObject.Highlight.gameObject.SetActive(false);
            }
        }
    }

    private void HighlightObject(HighlightObject highlightObject, bool highlight)
    {
        if (highlight)
        {
            if (highlightObject.Highlight != null)
            {
                highlightObject.Highlight.gameObject.SetActive(true);
            }
            else if (highlightObject.Object != null)
            {
                var renderer = highlightObject.Object.GetComponent<Renderer>();

                if (renderer != null && renderer.material != null)
                {
                    renderer.material.color = _highlightColor;
                }
            }

            highlightObject.Highlighted = true;
        }
        else
        {
            if (highlightObject.Highlight != null)
            {
                highlightObject.Highlight.gameObject.SetActive(false);
            }

            if (highlightObject.Object != null)
            {
                var renderer = highlightObject.Object.GetComponent<Renderer>();

                if (renderer != null && renderer.material != null)
                {
                    renderer.material.color = highlightObject.StandardColor;
                }
            }

            highlightObject.Highlighted = false;
        }
    }

    private void Update()
    {
        _ray.origin = transform.position;
        _ray.direction = transform.forward;

        _hits = Physics.RaycastAll(_ray.origin, _ray.direction, Mathf.Infinity);

        if (_hits != null)
        {
            foreach (var hit in _hits)
            {
                var highlightObject = _highlightObjects.Find(o => o.Object == hit.transform);

                if(highlightObject != null)
                {
                    HighlightObject(highlightObject, Time.time > highlightObject.HighlightTime + _minHighlightTime);
                }
            }
        }
    }
}
