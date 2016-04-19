using System;
using UnityEngine;

namespace peppar
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(InteractionOnMouseClickController), typeof(InteractionOnTouchClickController))]
    public class InteractionOnClickController : HandleInteractionController
    {
        [SerializeField]
        private bool _mouseInteraction = true;

        [SerializeField]
        private bool _touchInteraction = true;

        [SerializeField]
        private bool _checkInteraction = true;

        [SerializeField]
        private bool _showChildComponents = false;

        private InteractionOnMouseClickController _onMouseClickController;
        private InteractionOnTouchClickController _onTouchClickController;

        private bool _currentShowChildComponents = false;

        public bool MouseInteraction
        {
            get
            {
                return _mouseInteraction;
            }

            set
            {
                _mouseInteraction = value;
            }
        }

        public bool TouchInteraction
        {
            get
            {
                return _touchInteraction;
            }

            set
            {
                _touchInteraction = value;
            }
        }

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

        public bool IsTransformFirstObjectAtClickPosition(Transform wantedTransform)
        {
            InteractionType interactionType;

            return wantedTransform == GetFirstObjectAtClickPosition(out interactionType);
        }

        public Transform GetFirstObjectAtClickPosition(out InteractionType type)
        {
            Transform firstTransform = null;
            type = InteractionType.MouseClick;

            if (MouseInteraction)
            {
                firstTransform = _onMouseClickController.GetFirstObjectAtLeftMouseClickPosition();
            }

            if (firstTransform == null && TouchInteraction)
            {
                firstTransform = _onTouchClickController.GetFirstObjectAtFirstTouchPosition();
                type = InteractionType.TouchClick;
            }

            return firstTransform;
        }

        public Vector3 GetFirstHitPosAtClickPosition()
        {
            Vector3 firstPosition = new Vector3().Undefined();

            if (MouseInteraction)
                firstPosition = _onMouseClickController.GetFirstHitPosAtLeftMouseClickPosition();

            if (firstPosition.IsUndefined() && TouchInteraction)
                firstPosition = _onTouchClickController.GetFirstHitPosAtTouchPosition();

            return firstPosition;
        }

        public void HandleInteractions()
        {
            if (CheckInteraction == false)
                return;

            InteractionType interactionType;
            Transform interactionObject = GetFirstObjectAtClickPosition(out interactionType);

            HandleInteractions(interactionType, interactionObject);
        }

        protected override void Start()
        {
            _onMouseClickController.CheckInteraction = false;
            _onTouchClickController.CheckInteraction = false;
        }

        protected override void Update()
        {
            HandleInteractions();

            if (_currentShowChildComponents != _showChildComponents)
            {
                if (_showChildComponents == false)
                {
                    HideInChildComponentsInspector(_onMouseClickController, _onTouchClickController);
                }
                else
                {
                    ShowChildComponentsInInspector(_onMouseClickController, _onTouchClickController);
                }

                _currentShowChildComponents = _showChildComponents;
            }
        }

        protected override void Awake()
        {
            _onMouseClickController = GetComponent<InteractionOnMouseClickController>();
            _onTouchClickController = GetComponent<InteractionOnTouchClickController>();
        }
    }
}