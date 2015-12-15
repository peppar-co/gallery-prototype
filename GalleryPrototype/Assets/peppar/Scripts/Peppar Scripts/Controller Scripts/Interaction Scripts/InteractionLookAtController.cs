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

        protected override void Start()
        {
            
        }

        protected override void Update()
        {
            if (CheckInteraction)
                HandleInteractions(InteractionType.LookAt, _raycastController.FirstObjectAtScreenCenter);
        }

        protected override void Awake()
        {
            _raycastController = GetComponent<ScreenRaycastController>();

            HideInInspector(_raycastController);
        }
    }
}