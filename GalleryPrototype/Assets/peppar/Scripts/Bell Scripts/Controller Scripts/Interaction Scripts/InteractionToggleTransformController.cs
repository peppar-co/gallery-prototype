using UnityEngine;
using System.Collections;
using System;

namespace peppar.bell
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(ObjectTransformController), typeof(ObjectLerpController))]
    public class InteractionToggleTransformController : BehaviourController, InteractionFunctionality
    {
        [Serializable]
        protected class ToggleObject
        {
            public string Name = "Toggle Object";
            public Transform Transform;
            public Transform[] TargetTransforms = new Transform[2];

            public int CurrentTargetIndex { get; set; }

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
        private Transform[] _selfTargetTransforms = new Transform[2];

        [Space]

        [SerializeField]
        private ToggleObject[] _toggleObjects;

        [Space]

        [SerializeField]
        private bool _lerp;

        [SerializeField]
        private float _lerpSpeed = 10;

        private int _currentTargetIndex;

        private ObjectTransformController _transformController;
        private ObjectLerpController _lerpController;

        public void Interaction(InteractionState interactionState, InteractionType interationType)
        {
            if (interactionState == InteractionState.OnStart &&
                (interationType == InteractionType.MouseClick || interationType == InteractionType.TouchClick))
            {
                TogglePosition();
            }
        }

        private void TogglePosition()
        {
            if (_currentTargetIndex >= _selfTargetTransforms.Length - 1)
                _currentTargetIndex = 0;
            else
                _currentTargetIndex++;

            if (_lerp)
            {
                if (_selfTargetTransforms[_currentTargetIndex] != null)
                    _lerpController.TargetTransform = _selfTargetTransforms[_currentTargetIndex];

                foreach (var otherObject in _toggleObjects)
                {
                    if (otherObject.CurrentTargetIndex >= otherObject.TargetTransforms.Length - 1)
                        otherObject.CurrentTargetIndex = 0;
                    else
                        otherObject.CurrentTargetIndex++;

                    if (otherObject.LerpController != null && otherObject.TargetTransforms[otherObject.CurrentTargetIndex] != null)
                        otherObject.LerpController.TargetTransform = otherObject.TargetTransforms[otherObject.CurrentTargetIndex];
                }
            }
            else
            {
                _transformController.SetToTransform(_selfTargetTransforms[_currentTargetIndex]);

                foreach (var otherObject in _toggleObjects)
                {
                    if (otherObject.CurrentTargetIndex >= otherObject.TargetTransforms.Length - 1)
                        otherObject.CurrentTargetIndex = 0;
                    else
                        otherObject.CurrentTargetIndex++;

                    if (otherObject.TransformController != null && otherObject.TargetTransforms[otherObject.CurrentTargetIndex] != null)
                        otherObject.TransformController.SetToTransform(otherObject.TargetTransforms[otherObject.CurrentTargetIndex]);
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

            HideInInspector(_transformController, _lerpController);
        }
    }
}