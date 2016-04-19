using System;
using UnityEngine;

namespace peppar
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(ScreenRaycastController))]
    public class InteractionLookAtController : HandleInteractionController
    {
        [SerializeField]
        private bool _checkInteraction = true;

        [SerializeField]
        private bool _showViewTargetPosition = false;

        [SerializeField]
        private GameObject _viewTargetObject;

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

        public bool ShowViewTargetPosition
        {
            get
            {
                return _showViewTargetPosition;
            }

            set
            {
                _showViewTargetPosition = value;
            }
        }

        protected override void Start()
        {

        }

        protected override void Update()
        {
            if (CheckInteraction == false && ShowViewTargetPosition == false)
            {
                return;
            }

            var firstHitAtScreenCenter = _raycastController.FirstHitAtScreenCenter;

            if (CheckInteraction)
                HandleInteractions(InteractionType.LookAt, firstHitAtScreenCenter.transform);

            if (_viewTargetObject && ShowViewTargetPosition)
            {
                _viewTargetObject.SetActive(true);
                _viewTargetObject.transform.position = firstHitAtScreenCenter.point != Vector3.zero ? firstHitAtScreenCenter.point : new Vector3().Undefined();
            }
            else if(_viewTargetObject)
            {
                _viewTargetObject.SetActive(false);
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