using System;
using UnityEngine;
namespace peppar
{
    public class ObjectPositionLerpController : BehaviourController, LerpFunctionality
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

        public Vector3 TargetPosition
        {
            get
            {
                if (TargetTransform != null)
                    return TargetTransform.position;
                else
                    return transform.position;
            }
            set
            {
                TargetTransform.position = value;
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

        public Vector3 Position
        {
            get { return transform.position; }
        }

        public float DistanceToTarget
        {
            get { return Vector3.Distance(Position, TargetPosition); }
        }

        public void SetToTargetAndStopLerp()
        {
            transform.position = TargetPosition;
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

            transform.position = Vector3.Lerp(Position, TargetPosition, LerpSpeed / 100);

            if (DistanceToTarget <= StopLerpDistance)
                SetToTargetAndStopLerp();
        }

        protected override void Awake()
        {

        }
    }
}