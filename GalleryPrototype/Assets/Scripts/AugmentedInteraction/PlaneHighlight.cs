using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlaneHighlight : MonoBehaviour
{

    [SerializeField]
    LayerMask _layerMask;

    [SerializeField]
    Color _color;

    Color _oldColor;

    Ray _ray;
    RaycastHit _hit;

    [SerializeField]
    RaycastHit[] _hits;

    GameObject _hitGO;
    Material _hitMat;

    List<RaycastHit> _bucketList = new List<RaycastHit>();

    // Use this for initialization
    void Awake()
    {
        _oldColor = Color.white;
        _oldColor.a = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        _ray.origin = transform.position;
        _ray.direction = transform.forward;

        _hits = Physics.RaycastAll(_ray.origin, _ray.direction, Mathf.Infinity);
        Debug.Log("length " + _hits.Length);

        if (_hits != null)
        {
            foreach (var hit in _hits)
            {
                if (hit.transform.gameObject.GetComponent<HitStuff>() != null)
                {
                    var hitStuff = hit.transform.gameObject.GetComponent<HitStuff>();
                    hitStuff.GotHit = true;

                    _bucketList.Add(hit);
                    if (hit.transform.gameObject.GetComponent<Renderer>().material != null)
                        hit.transform.gameObject.GetComponent<Renderer>().material.color = _color;
                }
            }
        }
        //else
        //{
        //    Debug.Log("clearing bucketlist");
        //    foreach (var hit in _bucketList)
        //    {
        //        if (hit.transform.gameObject.GetComponent<Renderer>().material != null)
        //            hit.transform.gameObject.GetComponent<Renderer>().material.color = _oldColor;
        //    }
        //    _bucketList.Clear();
        //}


        //if (Physics.Raycast(_ray, out _hit, Mathf.Infinity))
        //{
        //    _hitGO = _hit.transform.gameObject;

        //    _hitMat = _hitGO.GetComponent<Renderer>().material;

        //    _hitMat.color = _color;
        //    Debug.Log("hit " + _hitGO.name);
        //}
        //else
        //{

        //    if (_hitMat != null)
        //        _hitMat.color = _oldColor;
        //    else
        //        Debug.Log("nothing hit with mat");

        //}



    }
}
