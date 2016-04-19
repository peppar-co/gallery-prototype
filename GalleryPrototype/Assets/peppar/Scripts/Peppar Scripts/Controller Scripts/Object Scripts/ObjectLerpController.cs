using UnityEngine;

namespace peppar
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(ObjectPositionLerpController), typeof(ObjectRotationLerpController))]
    public class ObjectLerpController : BehaviourController, LerpFunctionality
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

        public Transform TargetTransform
        {
            get
            {
                return _targetTransform;
            }

            set
            {
                _targetTransform = value;
                _positionLerpController.TargetTransform = value;
                _rotationLerpController.TargetTransform = value;
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

        public Quaternion Rotation
        {
            get
            {
                return _rotationLerpController.Rotation;
            }
        }

        public float DistanceToTarget
        {
            get
            {
                return _positionLerpController.DistanceToTarget;
            }
        }

        public void SetToTargetAndStopLerp()
        {
            _positionLerpController.SetToTargetAndStopLerp();
            _rotationLerpController.SetToTargetAndStopLerp();
        }

        protected override void Start()
        {

        }

        protected override void Update()
        {
#if UNITY_EDITOR
            Lerp = _lerp;
            TargetTransform = _targetTransform;
            LerpSpeed = _lerpSpeed;
            StopLerpDistance = _stopLerpDistance;
#endif
        }

        protected override void Awake()
        {
            _positionLerpController = GetComponent<ObjectPositionLerpController>();
            _rotationLerpController = GetComponent<ObjectRotationLerpController>();

            HideInChildComponentsInspector(_positionLerpController, _rotationLerpController);
        }
    }
}