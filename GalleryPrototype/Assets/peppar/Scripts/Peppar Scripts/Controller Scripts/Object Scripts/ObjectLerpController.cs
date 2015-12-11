using UnityEngine;
using System.Collections;
using System;

namespace peppar
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(ObjectPositionLerpController), typeof(ObjectRotationLerpController))]
    public class ObjectLerpController : BehaviourController, LerpFunctionality
    {
        [SerializeField]
        private bool _lerp = true;

        [SerializeField]
        private Transform _goalTransform;

        [SerializeField]
        [Range(0, 100)]
        private float _lerpSpeed = 10;

        [SerializeField]
        [Range(-1, 10)]
        private float _stopLerpDistance = 0.001f;

        private ObjectPositionLerpController _positionLerpController;
        private ObjectRotationLerpController _rotationLerpController;

        public bool Lerp
        {
            get
            {
                return _lerp;
            }

            set
            {
                _lerp = value;
                _positionLerpController.Lerp = value;
                _rotationLerpController.Lerp = value;
            }
        }

        public Transform GoalTransform
        {
            get
            {
                return _goalTransform;
            }

            set
            {
                _goalTransform = value;
                _positionLerpController.GoalTransform = value;
                _rotationLerpController.GoalTransform = value;
            }
        }

        public float LerpSpeed
        {
            get
            {
                return _lerpSpeed;
            }

            set
            {
                _lerpSpeed = value;
                _positionLerpController.LerpSpeed = value;
                _rotationLerpController.LerpSpeed = value;
            }
        }

        public float StopLerpDistance
        {
            get
            {
                return _stopLerpDistance;
            }

            set
            {
                _stopLerpDistance = value;
                _positionLerpController.StopLerpDistance = value;
                _rotationLerpController.StopLerpDistance = value;
            }
        }

        public Vector3 Position
        {
            get
            {
                return _positionLerpController.Position;
            }
        }

        public Vector3 Rotation
        {
            get
            {
                return _rotationLerpController.Rotation;
            }
        }

        public float DistanceToGoal
        {
            get
            {
                return _positionLerpController.DistanceToGoal;
            }
        }

        public void SetToGoalAndStopLerp()
        {
            _positionLerpController.SetToGoalAndStopLerp();
            _rotationLerpController.SetToGoalAndStopLerp();
        }

        protected override void Start()
        {
            _positionLerpController = GetComponent<ObjectPositionLerpController>();
            _rotationLerpController = GetComponent<ObjectRotationLerpController>();

            HideInInspector(_positionLerpController, _rotationLerpController);
        }

        protected override void Update()
        {
#if UNITY_EDITOR
            Lerp = _lerp;
            GoalTransform = _goalTransform;
            LerpSpeed = _lerpSpeed;
            StopLerpDistance = _stopLerpDistance;
#endif
        }
    }
}