using UnityEngine;
using System.Collections;
using System;

namespace peppar.bell
{
    public class InteractionTimeActivationController : BehaviourController, InteractionFunctionality
    {
        [SerializeField]
        private InteractionState _startTimeOnState = InteractionState.OnStop;

        [SerializeField]
        [Range(0.001f, 30f)]
        private float _timeTillInactivation = 1f;

        [SerializeField]
        private GameObject[] _activationObjects = new GameObject[1];

        private float _stopLookAtTime;

        public void Interaction(InteractionState interactionState, InteractionType interactionType)
        {
            if (interactionState == InteractionState.OnStart)
                SetActive();

            if (interactionState != _startTimeOnState)
                _stopLookAtTime = _timeTillInactivation;
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

        protected override void Awake()
        {

        }

        protected override void Start()
        {

        }

        protected override void Update()
        {
            if (_stopLookAtTime > 0)
            {
                _stopLookAtTime -= Time.deltaTime;
            }
            else if (_stopLookAtTime < 0)
            {
                SetInactive();
                _stopLookAtTime = 0;
            }
        }
    }
}
