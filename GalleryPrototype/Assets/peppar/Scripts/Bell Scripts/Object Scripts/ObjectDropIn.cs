using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectDropIn : MonoBehaviour
{

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
        _activated = false;

        foreach (var go in _gameObjectList)
        {
            _defaulPositions.Add(go.transform.position);
        }
    }

    private void ShufflePositions()
    {
        for (int i = 0; i < _defaulPositions.Count; i++)
        {
            _gameObjectList[i].transform.position = _defaulPositions[i] + (Vector3.up * Random.Range(20, 100));
        }
    }

    private void MoveToDefaultPosition()
    {
        for (int i = 0; i < _defaulPositions.Count; i++)
        {
            _gameObjectList[i].transform.position = Vector3.Lerp(_gameObjectList[i].transform.position, _defaulPositions[i], _speed);
        }
    }
    private void Update()
    {
        if (_activated)
        {
            MoveToDefaultPosition();
        }

        if (_shuffle)
        {
            ShufflePositions();
            _shuffle = false;
            //_activated = false;
        }
    }
}
