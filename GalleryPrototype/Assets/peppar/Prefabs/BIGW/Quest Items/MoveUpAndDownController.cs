using UnityEngine;
using System.Collections;
using System;

namespace peppar
{
    public class MoveUpAndDownController : BehaviourController
    {

        [SerializeField]
        private float _moveDistance = 0.1f;

        ObjectPositionLerpController _lerpController;

        private bool _up;

        protected override void Awake()
        {

        }

        protected override void Start()
        {
            _lerpController = transform.GetOrAddComponent<ObjectPositionLerpController>();
            _lerpController.StopLerpDistance = -1;
            _lerpController.Lerp = true;
        }

        protected override void Update()
        {
            if (_lerpController.DistanceToTarget < 0.01f)
            {
                if (_up)
                {
                    _lerpController.TargetPosition = new Vector3(transform.position.x, transform.position.y - _moveDistance, transform.position.z);
                    _up = false;
                }
                else
                {
                    _lerpController.TargetPosition = new Vector3(transform.position.x, transform.position.y + _moveDistance, transform.position.z);
                    _up = true;
                }
            }
        }
    }
}
