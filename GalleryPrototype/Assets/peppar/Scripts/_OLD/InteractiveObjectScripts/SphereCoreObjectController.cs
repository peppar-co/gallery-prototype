using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SphereCoreObjectController : MonoBehaviour
{

    [SerializeField]
    private List<SphereHullObject> _hullObjects = new List<SphereHullObject>();



    private bool _isActive;

    public bool IsActive
    {
        get { return _isActive; }
        set
        {
            _isActive = value;

            if (_isActive == false)
            {
                ResetInteractions();

                foreach (var hull in _hullObjects)
                {
                    hull.ResetObject();
                }
            }
        }
    }
    private bool _isEmpty;


    Transform _defaultTransform;

    private void Awake()
    {
        _defaultTransform = transform;
        ResetInteractions();

        
    }

    private void Update()
    {
        CheckPosition();
        CheckHull();
    }

    private void CheckHull()
    {
        if (_isActive)
        {
            foreach (var hull in _hullObjects)
            {
                if (hull.GotHit == false)
                {
                    return;
                }
            }
            Debug.Log("Why you no turn RED!?");
            ShowEyeBall();
        }
    }

    private void CheckPosition()
    {
        if (_isActive)
        {
            //transform.position = new Vector3(
            //                                    Mathf.MoveTowards(transform.position.x, _defaultTransform.position.x, 1),
            //                                    Mathf.MoveTowards(transform.position.y + 1, _defaultTransform.position.y, 1),
            //                                    Mathf.MoveTowards(transform.position.z, _defaultTransform.position.z, 1)
            //                                    );
            transform.localPosition = new Vector3(0, 2, 0);
        }
        else
        {
            //transform.position = new Vector3(
            //                                    Mathf.MoveTowards(transform.position.x, _defaultTransform.position.x, 1),
            //                                    Mathf.MoveTowards(transform.position.y, _defaultTransform.position.y + 1, 1),
            //                                    Mathf.MoveTowards(transform.position.z, _defaultTransform.position.z, 1)
            //                                    );
            transform.localPosition = new Vector3(0, 5, 0);
        }
    }

    private void ShowEyeBall()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }


    public void ActivateInteraction()
    {
        _isActive = true;
    }


    public void ResetInteractions()
    {
        _isActive = false;
        _isEmpty = false;
        transform.localPosition = _defaultTransform.localPosition;
        transform.rotation = _defaultTransform.rotation;

        
        GetComponent<Renderer>().material.color = Color.white;
        //transform.localRotation = _defaultTransform.localRotation;
               

    }



}
