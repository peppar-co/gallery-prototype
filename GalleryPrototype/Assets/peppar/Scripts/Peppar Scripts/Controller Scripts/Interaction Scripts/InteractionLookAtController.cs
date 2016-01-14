using System;
using UnityEngine;

namespace peppar
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(ScreenRaycastController))]
    public class InteractionLookAtController : InteractionController
    {
        [SerializeField]
        private bool _checkInteraction = true;

        [SerializeField]
        private bool _showViewTargetPosition = false;

        [SerializeField]
        private GameObject _viewTargetObject;

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
                return;

            var firstHitAtScreenCenter = _raycastController.FirstHitAtScreenCenter;

            if (CheckInteraction)
                HandleInteractions(InteractionType.LookAt, firstHitAtScreenCenter.transform);

            if (_viewTargetObject && ShowViewTargetPosition)
                _viewTargetObject.transform.position = firstHitAtScreenCenter.point != Vector3.zero ? firstHitAtScreenCenter.point : new Vector3().Undefined();
        }

        protected override void Awake()
        {
            _raycastController = GetComponent<ScreenRaycastController>();

            HideInInspector(_raycastController);
        }
    }
}