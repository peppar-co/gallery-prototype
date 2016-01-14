using UnityEngine;
using System.Collections;
using System;

namespace peppar.bell
{
    public class InteractionToggleActivationController : BehaviourController, InteractionFunctionality
    {
        [SerializeField]
        private bool _onlyToggleOnLookAt = false;

        [SerializeField]
        private GameObject[] _activationObjects = new GameObject[1];

        public void Interaction(InteractionState interactionState, InteractionType interactionType)
        {
            if (interactionState == InteractionState.Start)
                ToggleActivation();

            if (_onlyToggleOnLookAt && interactionState == InteractionState.Stop)
                ToggleActivation();
        }

        private void ToggleActivation()
        {
            foreach (var activationObject in _activationObjects)
                if (activationObject != null)
                    activationObject.SetActive(!activationObject.activeSelf);
        }

        protected override void Start()
        {

        }

        protected override void Update()
        {

        }

        protected override void Awake()
        {

        }
    }
}