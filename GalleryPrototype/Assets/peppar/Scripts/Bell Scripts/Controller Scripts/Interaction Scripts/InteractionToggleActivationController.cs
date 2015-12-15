using UnityEngine;
using System.Collections;
using System;

namespace peppar.bell
{
    public class InteractionToggleActivationController : BehaviourController, InteractionFunctionality
    {
        [SerializeField]
        private GameObject[] _activationObjects = new GameObject[1];

        public void Interaction(InteractionState interactionState, InteractionType interactionType)
        {
            if (interactionState == InteractionState.Start)
                ToggleActivation();
        }

        private void ToggleActivation()
        {
            foreach (var activationObject in _activationObjects)
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