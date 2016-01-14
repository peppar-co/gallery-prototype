using UnityEngine;
using System.Collections;
using System;

namespace peppar.bell
{
    public class InteractionToggleActivationController : BehaviourController, InteractionFunctionality
    {
        [SerializeField]
        private bool _onlyActiveOnLookAt = false;

        [SerializeField]
        private GameObject[] _activationObjects = new GameObject[1];

        public void Interaction(InteractionState interactionState, InteractionType interactionType)
        {
            if (interactionState == InteractionState.Start)
                ToggleActivation();

            if (_onlyActiveOnLookAt && interactionState == InteractionState.Stop)
                SetInactive();

        }

        private void ToggleActivation()
        {
            foreach (var activationObject in _activationObjects)
                if (activationObject != null)
                    activationObject.SetActive(!activationObject.activeSelf);
        }

        private void SetActive()
        {
            foreach (var activationObject in _activationObjects)
                if (activationObject != null)
                    activationObject.SetActive(true);
        }

        private void SetInactive()
        {
            foreach (var activationObject in _activationObjects)
                if (activationObject != null)
                    activationObject.SetActive(false);
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