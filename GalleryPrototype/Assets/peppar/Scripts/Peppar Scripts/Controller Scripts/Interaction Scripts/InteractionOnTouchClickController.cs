using UnityEngine;

namespace peppar
{
    [ExecuteInEditMode]
    public class InteractionOnTouchClickController : InteractionController
    {
        [SerializeField]
        private bool _checkInteraction = true;

        private ScreenRaycastController _raycastController;

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
            _raycastController = GetComponent<ScreenRaycastController>();

            HideInInspector(_raycastController);
        }

        protected override void Update()
        {
            if(CheckInteraction)
            HandleInteractions(InteractionType.TouchClick, GetFirstObjectAtFirstTouchPosition());
        }
    }
}