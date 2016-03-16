using UnityEngine;
using System.Collections;
using System;

namespace peppar.bell
{
    public class InteractionToggleActivationController : BehaviourController, InteractionFunctionality
    {
        [SerializeField]
        private InteractionState _toggleOnState = InteractionState.Start;

        [SerializeField]
        private bool _onlyToggleOnLookAt = false;

        [SerializeField]
        private GameObject[] _activationObjects = new GameObject[1];

        public void Interaction(InteractionState interactionState, InteractionType interactionType)
        {
            if (_onlyToggleOnLookAt == false && interactionState == _toggleOnState)
                ToggleActivation();
            else if(interactionState == InteractionState.Start)
                ToggleActivation();

            if (_onlyToggleOnLookAt && _toggleOnState != interactionState && interactionState == InteractionState.Stop)
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