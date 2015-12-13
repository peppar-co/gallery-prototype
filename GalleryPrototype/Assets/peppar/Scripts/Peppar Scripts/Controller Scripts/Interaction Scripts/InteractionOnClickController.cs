using UnityEngine;

namespace peppar
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(InteractionOnMouseClickController), typeof(InteractionOnTouchClickController))]
    public class InteractionOnClickController : InteractionController
    {
        [SerializeField]
        private bool _mouseInteraction = true;

        [SerializeField]
        private bool _touchInteraction = true;

        [SerializeField]
        private bool _checkInteraction = true;

        private InteractionOnMouseClickController _onMouseClickController;
        private InteractionOnTouchClickController _onTouchClickController;

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
            _onMouseClickController = GetComponent<InteractionOnMouseClickController>();
            _onTouchClickController = GetComponent<InteractionOnTouchClickController>();

            _onMouseClickController.CheckInteraction = false;
            _onTouchClickController.CheckInteraction = false;

            HideInInspector(_onMouseClickController, _onTouchClickController);
        }

        protected override void Update()
        {
            HandleInteractions();
        }
    }
}