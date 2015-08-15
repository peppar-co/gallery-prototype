using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class HighlightObject
{
    [SerializeField]
    private Transform _collider;

    [SerializeField]
    private Transform _highlight;

    private bool _highlighted;

    private float _highlightTime = int.MinValue;

    public bool Highlighted
    {
        get { return _highlighted; }
    }

    public float HighlightTime
    {
        get { return _highlightTime; }
    }

    public Transform Collider
    {
        get { return _collider; }
    }

    public void Highlight()
    {
        if (_highlight != null)
        {
            _highlight.gameObject.SetActive(true);
            _highlightTime = Time.time;
            _highlighted = true;
        }
    }

    public void DeHighlight()
    {
        Debug.Log("DEHIGHLIGHT");

        if (_highlight != null)
        {
            _highlight.gameObject.SetActive(false);
            _highlighted = false;
        }
    }

    //public Color StandardColor
    //{
    //    get; set;
    //}
}

public class HighlightSystem : MonoBehaviour
{
    //[SerializeField]
    //private LayerMask _layerMask;

    //[SerializeField]
    //private Color _highlightColor;

    [SerializeField]
    private List<HighlightObject> _highlightObjects;

    [SerializeField]
    private float _minHighlightTime;

    [SerializeField]
    RaycastHit[] _hits;

    private Ray _ray;
    private RaycastHit _hit;

    private float deltaTime;

    List<RaycastHit> _raycastHitList = new List<RaycastHit>();

    private void Awake()
    {
        if (_highlightObjects == null)
        {
            _highlightObjects = new List<HighlightObject>();
        }

        foreach (var highlight in _highlightObjects)
        {
            highlight.DeHighlight();
        }
        //foreach (var highlightobject in _highlightObjects)
        //{
        //    if (highlightobject.Highlight != null)
        //    {
        //        var renderer = highlightobject.hi.getcomponent<renderer>();

        //        if (renderer != null && renderer.material != null)
        //        {
        //            highlightobject.standardcolor = renderer.material.color;
        //        }

        //        highlightobject.highlight.gameobject.setactive(true);
        //    }
        //}
    }

    private void HighlightObject(HighlightObject highlightObject, bool highlight)
    {
        if (highlight)
        {

            foreach (var highlighted in _highlightObjects)
            {
                highlighted.DeHighlight();
            }
            highlightObject.Highlight();

            //else if (highlightObject.Object != null)
            //{
            //    var renderer = highlightObject.Object.GetComponent<Renderer>();

            //    if (renderer != null && renderer.material != null)
            //    {
            //        renderer.material.color = _highlightColor;
            //    }
            //}

            //highlightObject.Highlighted = true;
        }
        //else
        //{

        //        //highlighted.Highlighted = false;
        //    //if (highlightObject.Highlight != null)
        //    //{
        //    //    highlightObject.Highlight.gameObject.SetActive(false);
        //    //}

        //    //if (highlightObject.Object != null)
        //    //{
        //    //    var renderer = highlightObject.Object.GetComponent<Renderer>();

        //    //    if (renderer != null && renderer.material != null)
        //    //    {
        //    //        renderer.material.color = highlightObject.StandardColor;
        //    //    }
        //    //}

        //    //highlightObject.Highlighted = false;
        //}
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
                var highlightObject = _highlightObjects.Find(o => o.Collider == hit.transform);

                if (highlightObject != null)
                {
                    //HighlightObject(highlightObject, Time.time > highlightObject.HighlightTime + _minHighlightTime);
                    HighlightObject(highlightObject, true);
                    //Debug.Log("tiiimmeee:" + (Time.time > highlightObject.HighlightTime + _minHighlightTime));
                    //Debug.Log(Time.time);
                }
            }
        }
    }
}
