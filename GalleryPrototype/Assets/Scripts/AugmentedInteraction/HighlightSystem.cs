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

    public bool Highlighted
    {
        get { return _highlighted; }
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
            _highlighted = true;
        }
    }

    public void DeHighlight()
    {
        if (_highlight != null)
        {
            _highlight.gameObject.SetActive(false);
            _highlighted = false;
        }
    }

}

public class HighlightSystem : MonoBehaviour
{
    [SerializeField]
    Collider _hideCollider;
    private bool _hideColliderButtonShown;

    [SerializeField]
    private List<HighlightObject> _highlightObjects;

    public List<HighlightObject> HighlightObjects
    {
        get { return _highlightObjects; }
    }

    [SerializeField]
    private float _minHighlightTime;
    private float deltaTime;

    [SerializeField]
    private RectTransform _crosshair;

    RaycastHit[] _hits;

    private Ray _ray;
    private RaycastHit _hit;

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

        _hideColliderButtonShown = false;
        _hideCollider.gameObject.SetActive(_hideColliderButtonShown);
        deltaTime = 1;
    }

    private void HighlightObject(HighlightObject highlightObject, bool highlight)
    {
        if (highlight)
        {
            highlightObject.Highlight();
            _hideColliderButtonShown = true;
        }
    }

    public void DeHighlightObjects()
    {
        foreach (var highlighted in _highlightObjects)
        {
            highlighted.DeHighlight();
        }
        _hideColliderButtonShown = false;
    }

    public void ScaleCrosshair(HighlightObject highlightObject)
    {
        //TODO scale crosshair correctly - KN
    }

    private void Update()
    {
        _ray.origin = transform.position;
        _ray.direction = transform.forward;
        _hideCollider.gameObject.SetActive(_hideColliderButtonShown);

        _hits = Physics.RaycastAll(_ray.origin, _ray.direction, Mathf.Infinity);

        if (_hits != null)
        {
            foreach (var hit in _hits)
            {
                var highlightObject = _highlightObjects.Find(o => o.Collider == hit.transform);
                Debug.Log(hit.transform.gameObject.name);
                if (hit.transform.gameObject.name == "Hide")
                {
                    DeHighlightObjects();
                    _crosshair.localScale = Vector3.one;
                }

                if (highlightObject != null && _crosshair != null)
                {
                    ScaleCrosshair(highlightObject);
                    HighlightObject(highlightObject, true);
                }
                else
                {
                    Debug.LogWarning("No crosshair specified  or Nothing hit");
                }

            }
        }
    }
}
