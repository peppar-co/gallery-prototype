using System;
using UnityEngine;

namespace peppar
{
    [ExecuteInEditMode]
    public class ObjectLookAtController : BehaviourController
    {
        [SerializeField]
        private bool _transformIsTarget = true;

        [SerializeField]
        private Transform _lookAtTarget;

        [SerializeField]
        private Vector3 _lookAtPosition;

        [Header("Rotation axes")]

        [SerializeField]
        private bool _x = true;
        [SerializeField]
        private bool _y = true;
        [SerializeField]
        private bool _z = true;

        [Space]

        [SerializeField]
        private bool _smooth = true;

        [SerializeField]
        [Range(0, 100)]
        private float _lerpSpeed = 10;

        public Transform LookAtTarget
        {
            get
            {
                return _lookAtTarget;
            }

            set
            {
                _lookAtTarget = value;
                _transformIsTarget = true;
            }
        }

        public Vector3 LookAtPosition
        {
            get
            {
                return _lookAtPosition;
            }

            set
            {
                _lookAtPosition = value;
                _transformIsTarget = false;
            }
        }

        public bool Smooth
        {
            get
            {
                return _smooth;
            }

            set
            {
                _smooth = value;
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

        private void LookAt(Vector3 position)
        {
            Vector3 rotationBefore = transform.rotation.eulerAngles;

            if (Smooth)
            {
                Quaternion targetRotation = Quaternion.LookRotation(position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, LerpSpeed / 10 * Time.deltaTime);
            }
            else
            {
                transform.LookAt(position);
            }

            CorrectStaticAxes(rotationBefore);
        }

        private void LookAt(Transform target)
        {
            if (target == null)
            {
                Debug.LogError("ObjectLookAtController: LookAtTarget is null");
                return;
            }

            LookAt(target.position);
        }

        private void CorrectStaticAxes(Vector3 rotationBefore)
        {
            Vector3 newRotation = transform.rotation.eulerAngles;

            newRotation = new Vector3(
                _x ? newRotation.x : rotationBefore.x,
                _y ? newRotation.y : rotationBefore.y,
                _z ? newRotation.z : rotationBefore.z);

            transform.rotation = Quaternion.Euler(newRotation);
        }

        protected override void Start()
        {

        }

        protected override void Update()
        {
            if (_transformIsTarget)
                LookAt(LookAtTarget);
            else
                LookAt(LookAtPosition);
        }

        protected override void Awake()
        {

        }
    }
}