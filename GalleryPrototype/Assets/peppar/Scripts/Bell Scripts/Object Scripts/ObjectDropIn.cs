using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectDropIn : MonoBehaviour
{


    [SerializeField, Range(0, 1)]
    private float _colorLerp;


    [SerializeField]
    private GameObject _referenceObject;

    private Vector3 _referenceObjectDefaultPosition;

    [SerializeField]
    private Material _triggerMaterial;


    [SerializeField]
    private bool _activated;

    [SerializeField]
    private bool _shuffle;

    [SerializeField]
    private List<GameObject> _gameObjectList = new List<GameObject>();

    private List<Vector3> _defaulPositions = new List<Vector3>();

    [SerializeField, RangeAttribute(0f, 0.1f)]
    private float _speed;

    private void Awake()
    {
        UnityEngine.Assertions.Assert.IsNotNull(_gameObjectList);
        UnityEngine.Assertions.Assert.IsNotNull(_referenceObject);
        UnityEngine.Assertions.Assert.IsNotNull(_triggerMaterial);
        _activated = false;

        foreach (var go in _gameObjectList)
        {
            _defaulPositions.Add(go.transform.position);
        }

        _referenceObjectDefaultPosition = _referenceObject.transform.position;
    }

    private void ShufflePositions()
    {
        for (int i = 0; i < _defaulPositions.Count; i++)
        {
            _gameObjectList[i].transform.position = _defaulPositions[i] + (Vector3.up * Random.Range(20, 100));
        }

        _referenceObject.transform.position = _referenceObjectDefaultPosition - Vector3.up * 20;
    }

    private void MoveToDefaultPosition()
    {
        for (int i = 0; i < _defaulPositions.Count; i++)
        {
            _gameObjectList[i].transform.position = Vector3.Lerp(_gameObjectList[i].transform.position, _defaulPositions[i], _speed);
        }

        _referenceObject.transform.position = Vector3.Lerp(_referenceObject.transform.position, _referenceObjectDefaultPosition, _speed * 2);
    }
    private void Update()
    {
        if (_activated)
        {
            MoveToDefaultPosition();
            _referenceObject.SetActive(true);
            Destroy(GetComponent<MeshRenderer>());
        }
        else
        {
            _referenceObject.SetActive(false);

            var meshRenderer = GetComponent<MeshRenderer>();

            if (meshRenderer != null)
            {
                meshRenderer.material = _triggerMaterial;
                Color lerpColor = Color.Lerp(Color.red, Color.green, Mathf.PingPong(Time.time, _colorLerp));
                lerpColor.a = Mathf.PingPong(Time.time, _colorLerp);
                meshRenderer.material.color = lerpColor;
            }
            else
            {
                gameObject.AddComponent<MeshRenderer>();
            }
        }

        if (_shuffle)
        {
            ShufflePositions();
            _shuffle = false;
            //_activated = false;
        }



        //get raycast from touch (android)       
        for (var i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                // Construct a ray from the current touch coordinates
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);

                //utlDebugPrint.Inst.print(Input.GetTouch(i).position.ToString());

                if (Physics.Raycast(ray))
                {
                    _shuffle = true;
                    _activated = !_activated;
                    utlDebugPrint.Inst.print(_activated.ToString());
                }
            }
        }


        //mouse interaction for PC debugging
        if (Input.GetMouseButtonDown(0))
        {
            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            Debug.Log(Input.mousePosition);

            if (Physics.Raycast(mRay))
            {
                _shuffle = true;
                _activated = !_activated;
            }
        }
    }









}
