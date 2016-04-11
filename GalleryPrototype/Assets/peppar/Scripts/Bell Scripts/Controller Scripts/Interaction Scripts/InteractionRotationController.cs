using UnityEngine;
using System.Collections;
using System;

namespace peppar.bell
{
    public class InteractionRotationController : BehaviourController, InteractionFunctionality
    {
        [SerializeField]
        private float _rotationSpeed = 1f;

        [SerializeField]
        private GameObject[] _activationObjects = new GameObject[1];

        public void Interaction(InteractionState interactionState, InteractionType interactionType)
        {
            if (interactionState == InteractionState.OnHover)
                Rotate();
        }

        private void Rotate()
        {
            foreach (var activationObject in _activationObjects)
                activationObject.transform.Rotate(activationObject.transform.up, _rotationSpeed);
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