﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace peppar.bell
{
    public class InteractionToggleActivationController : BehaviourController, InteractionFunctionality
    {
        [SerializeField]
        private InteractionState _toggleOnState = InteractionState.Start;

        [SerializeField]
        private bool _onlyToggleOnLookAt = false;

        [SerializeField]
        private List<GameObject> _activationObjects = new List<GameObject>();

        public IEnumerable<GameObject> ActivationObjects
        {
            get
            {
                return _activationObjects;
            }
        }

        public void AddActivationObject(params GameObject[] activationObject)
        {
            _activationObjects.AddRange(activationObject);
        }

        public void RemoveActivationObject(params GameObject[] activationObject)
        {
            activationObject.ForEach(o => _activationObjects.Remove(o));
        }

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
            foreach (var activationObject in ActivationObjects)
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