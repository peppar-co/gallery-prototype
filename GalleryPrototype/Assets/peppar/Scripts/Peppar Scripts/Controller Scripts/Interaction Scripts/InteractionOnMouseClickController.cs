using UnityEngine;

namespace peppar
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(ScreenRaycastController))]
    public class InteractionOnMouseClickController : InteractionController
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

        public bool IsTranformFirstObjectAtLeftMouseClickPosition(Transform wantedObject)
        {
            return wantedObject == GetFirstObjectAtLeftMouseClickPosition();
        }

        public Transform GetFirstObjectAtLeftMouseClickPosition()
        {
            if (GetLeftMouseButtonDown())
            {
                return GetFirstObjectAtMousePosition();
            }

            return null;
        }

        public Vector3 GetFirstHitPosAtLeftMouseClickPosition()
        {
            if (GetLeftMouseButtonDown())
            {
                return GetFirstHitPosAtMousePosition();
            }

            return new Vector3().Undefined();
        }

        public bool IsTransformFirstObjectAtMousePosition(Transform wantedObject)
        {
            return wantedObject == GetFirstObjectAtMousePosition();
        }

        public Transform GetFirstObjectAtMousePosition()
        {
            return _raycastController.GetFirtsObjectAtScreenPosPixel(Input.mousePosition);
        }

        public Vector3 GetFirstHitPosAtMousePosition()
        {
            return _raycastController.GetFirstHitPosAtScreenPos(Input.mousePosition);
        }

        public bool GetLeftMouseButtonDown()
        {
            return Input.GetMouseButtonDown(0);
        }

        public bool GetRightMouseButtonDown()
        {
            return Input.GetMouseButtonDown(1);
        }

        public bool GetMiddleMouseButtonDown()
        {
            return Input.GetMouseButtonDown(2);
        }

        protected override void Start()
        {
            _raycastController = GetComponent<ScreenRaycastController>();

            HideInInspector(_raycastController);
        }

        protected override void Update()
        {
            if (CheckInteraction)
                HandleInteractions(InteractionType.MouseClick, GetFirstObjectAtLeftMouseClickPosition());
        }
    }
}