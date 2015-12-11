using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace peppar
{
    [RequireComponent(typeof(ScreenRaycastController))]
    public class InteractionLookAtController : BehaviourController
    {
        [SerializeField]
        private List<Collider> _interactionTriggers = new List<Collider>();

        [SerializeField]
        private List<BehaviourController> _interactionObjects = new List<BehaviourController>();

        [SerializeField]
        private readonly List<Transform> _foo = new List<Transform>();

        private ScreenRaycastController _raycastController;

        private bool _lookAtTrigger;

        public readonly List<InteractionFunctionality> InteractionObjects = new List<InteractionFunctionality>();

        private bool CheckForInteractionTrigger()
        {
            return _interactionTriggers.Exists(t => t == _raycastController.FirstObjectAtScreenCenter);
        }

        private void AddCoorectInteractionObjectsToList()
        {
            foreach (var interactionObject in _interactionObjects)
            {
                if (interactionObject is InteractionFunctionality)
                {
                    InteractionObjects.Add(interactionObject as InteractionFunctionality);
                }
            }
        }

        protected override void Start()
        {
            _raycastController = GetComponent<ScreenRaycastController>();

            HideInInspector(_raycastController);

            AddCoorectInteractionObjectsToList();
        }

        protected override void Update()
        {
            bool triggerCheck = CheckForInteractionTrigger();

            if (_lookAtTrigger == false && triggerCheck)
            {
                InteractionObjects.ForEach(o => o.StartAction());
            }

            if (_lookAtTrigger && triggerCheck == false)
            {
                InteractionObjects.ForEach(o => o.StopAction());
            }

            if (triggerCheck)
            {
                InteractionObjects.ForEach(o => o.DoAction());
            }

            _lookAtTrigger = triggerCheck;
        }
    }
}