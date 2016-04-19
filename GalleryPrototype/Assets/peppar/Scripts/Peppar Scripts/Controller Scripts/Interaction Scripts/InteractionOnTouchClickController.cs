using System;
using UnityEngine;

namespace peppar
{
    [ExecuteInEditMode]
    public class InteractionOnTouchClickController : HandleInteractionController
    {
        [SerializeField]
        private bool _checkInteraction = true;

        [SerializeField]
        private bool _showChildComponents = false;

        private ScreenRaycastController _raycastController;

        private bool _currentShowChildComponents = false;

        public bool CheckInteraction
        {
            get
            {
                return _checkInteraction;
            }

            set
            {
                _checkInteraction = value;
            }
        }

        public bool IsTransformFirstObjectAtFirstTouchPosition(Transform wantedTransform)
        {
            return wantedTransform == GetFirstObjectAtFirstTouchPosition();
        }

        public Transform GetFirstObjectAtFirstTouchPosition()
        {
            foreach (var touch in Input.touches)
            {
                return _raycastController.GetFirtsObjectAtScreenPosPixel(touch.position);
            }

            return null;
        }

        public Vector3 GetFirstHitPosAtTouchPosition()
        {
            foreach (var touch in Input.touches)
            {
                return _raycastController.GetFirstHitPosAtScreenPos(touch.position);
            }

            return new Vector3().Undefined();
        }

        protected override void Start()
        {

        }

        protected override void Update()
        {
            if (CheckInteraction)
            {
                HandleInteractions(InteractionType.TouchClick, GetFirstObjectAtFirstTouchPosition());
            }

            if (_currentShowChildComponents != _showChildComponents)
            {
                if (_showChildComponents == false)
                {
                    HideInChildComponentsInspector(_raycastController);
                }
                else
                {
                    ShowChildComponentsInInspector(_raycastController);
                }

                _currentShowChildComponents = _showChildComponents;
            }
        }

        protected override void Awake()
        {
            _raycastController = GetComponent<ScreenRaycastController>();
        }
    }
}