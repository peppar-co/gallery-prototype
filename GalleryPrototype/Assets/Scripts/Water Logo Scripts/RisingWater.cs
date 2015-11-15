using UnityEngine;
using System.Collections;

public class RisingWater : MonoBehaviour
{
    [SerializeField]
    private Transform _water;

    [SerializeField]
    private float _risingSpeed = 1;

    [SerializeField]
    private float _maxHight = 3;

    private bool _isWaterRising;

    private Vector3 _waterStartingPosition;

    // Use this for initialization
    private void Start()
    {
        UnityEngine.Assertions.Assert.IsNull(_water, "water transform is null");

        if (_water != null)
        {
            _waterStartingPosition = _water.position;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        CheckForStopWaterRising();
    }

    private void FixedUpdate()
    {
        if (_isWaterRising)
            WaterRising();
    }

    public void StartWaterRising()
    {
        _isWaterRising = true;
    }

    public void StartWaterRising(float speed)
    {
        _risingSpeed = speed;

        StartWaterRising();
    }

    private void WaterRising()
    {
        transform.position = Vector3.up * _risingSpeed / 100;
    }

    private void CheckForStopWaterRising()
    {
        if (_water.position.y - _waterStartingPosition.y > _maxHight)
            _isWaterRising = false;
    }
}
