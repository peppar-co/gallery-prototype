using UnityEngine;
using System.Collections;
using System;

namespace peppar
{
    public class ObjectRotationLerpController : BehaviourController, LerpFunctionality
    {
        [SerializeField]
        private bool _lerp = true;

        [SerializeField]
        private Transform _targetTransform;

        [SerializeField]
        [Range(0, 100)]
        private float _lerpSpeed = 10;

        [SerializeField]
        [Range(-1, 10)]
        private float _stopLerpDistance = 0.001f;

        public bool Lerp
        {
            get
            {
                return _lerp;
            }

            set
            {
                _lerp = value;
            }
        }

        public Transform TargetTransform
        {
            get
            {
                return _targetTransform;
            }

            set
            {
                _targetTransform = value;
                Lerp = true;
            }
        }

        public Vector3 GoalRotation
        {
            get
            {
                if (TargetTransform != null)
                    return TargetTransform.rotation.eulerAngles;
                else
                    return transform.rotation.eulerAngles;
            }
            set
            {
                TargetTransform.rotation = GetQuaternion(value);
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
            }
        }

        public Vector3 Rotation
        {
            get { return transform.rotation.eulerAngles; }
        }

        public Quaternion GetQuaternion(Vector3 vector)
        {
            return Quaternion.Euler(vector);
        }

        public float DistanceToTarget
        {
            get { return Mathf.Abs(Quaternion.Angle(GetQuaternion(Rotation), GetQuaternion(GoalRotation))); }
        }

        public void SetToTargetAndStopLerp()
        {
            transform.rotation = Quaternion.Euler(GoalRotation);
            Lerp = false;
        }

        protected override void Start()
        {

        }

        protected override void Update()
        {

        }

        private void FixedUpdate()
        {
            if (!Lerp)
                return;

            transform.rotation = GetQuaternion(Vector3.Lerp(Rotation, GoalRotation, LerpSpeed / 100));

            if (DistanceToTarget <= StopLerpDistance)
                SetToTargetAndStopLerp();
        }

        protected override void Awake()
        {

        }
    }
}