using UnityEngine;
using System.Collections;
using System;

namespace peppar.bell
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(ObjectTransformController), typeof(ObjectLerpController))]
    public class InteractionSetToTransformController : BehaviourController, InteractionFunctionality
    {
        [Serializable]
        private class OtherObject
        {
            public string Name = "Other Object";
            public Transform Transform = null;
            public Transform TargetTransform = null;

            public ObjectTransformController TransformController
            {
                get
                {
                    if (Transform != null)
                        return Transform.GetOrAddComponent<ObjectTransformController>();
                    else
                        return null;
                }
            }

            public ObjectLerpController LerpController
            {
                get
                {
                    if (Transform != null)
                        return Transform.GetOrAddComponent<ObjectLerpController>();
                    else
                        return null;
                }
            }
        }

        [SerializeField]
        private Transform _selfTargetTransform;

        [Space]

        [SerializeField]
        private OtherObject[] _otherObjects;

        [Space]

        [SerializeField]
        private bool _lerp;

        [SerializeField]
        private float _lerpSpeed = 10;

        private ObjectTransformController _transformController;
        private ObjectLerpController _lerpController;

        public virtual void Interaction(InteractionState interactionState, InteractionType interationType)
        {
            if (interactionState == InteractionState.OnStart &&
                (interationType == InteractionType.MouseClick || interationType == InteractionType.TouchClick))
            {
                SetToPosition();
            }
        }

        private void SetToPosition()
        {
            if (_lerp)
            {
                if (_selfTargetTransform != null)
                    _lerpController.TargetTransform = _selfTargetTransform;

                foreach (var otherObject in _otherObjects)
                {
                    if (otherObject.LerpController != null && otherObject.TargetTransform != null)
                        otherObject.LerpController.TargetTransform = otherObject.TargetTransform;
                }
            }
            else
            {
                _transformController.SetToTransform(_selfTargetTransform);

                foreach (var otherObject in _otherObjects)
                {
                    if (otherObject.TransformController != null)
                        otherObject.TransformController.SetToTransform(otherObject.TargetTransform);
                }
            }
        }

        protected override void Start()
        {
            _lerpController.LerpSpeed = _lerpSpeed;
        }

        protected override void Update()
        {

        }

        protected override void Awake()
        {
            _transformController = GetComponent<ObjectTransformController>();
            _lerpController = GetComponent<ObjectLerpController>();

            HideInChildComponentsInspector(_transformController, _lerpController);
        }
    }
}