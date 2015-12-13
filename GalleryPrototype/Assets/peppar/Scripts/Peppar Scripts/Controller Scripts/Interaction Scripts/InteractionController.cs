using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace peppar
{
    public enum InteractionType
    {
        LookAt,
        MouseClick,
        TouchClick
    }

    public enum InteractionState
    {
        Start,
        Do,
        Stop
    }

    public abstract class InteractionController : BehaviourController
    {
        protected Transform _currentInteractionObject;

        public bool IsObjectInteractive(Transform interactionObject)
        {
            var interactionComponets = GetInteractionComponents(interactionObject);

            return interactionComponets != null && interactionComponets.Count() > 0;
        }

        public IEnumerable<InteractionFunctionality> GetInteractionComponents(Transform interactionObject)
        {
            if (interactionObject == null)
                return null;

            return interactionObject.GetComponents(typeof(BehaviourController)).Where(c => c is InteractionFunctionality).Cast<InteractionFunctionality>();
        }

        protected void HandleInteractions(InteractionState state, InteractionType type, params InteractionFunctionality[] components)
        {
            if (components == null)
                return;

            components.ForEach(c => c.Interaction(state, type));
        }

        protected void HandleInteractions(InteractionState state, InteractionType type, Transform interactionObject)
        {
            if (interactionObject == null)
                return;

            HandleInteractions(state, type, GetInteractionComponents(interactionObject).ToArray());
        }

        protected void HandleInteractions(InteractionType type, Transform interactionObject)
        {
            if (interactionObject == _currentInteractionObject)
            {
                if (interactionObject != null)
                    HandleInteractions(InteractionState.Do, type, interactionObject);
            }
            else
            {
                if (_currentInteractionObject != null)
                    HandleInteractions(InteractionState.Stop, type, _currentInteractionObject);

                if (interactionObject != null)
                    HandleInteractions(InteractionState.Start, type, interactionObject);

                _currentInteractionObject = interactionObject;
            }
        }

        protected override void Start()
        {

        }

        protected override void Update()
        {

        }
    }
}