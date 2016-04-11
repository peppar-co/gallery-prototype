using UnityEngine;
using System.Linq;

namespace peppar
{
    public class BasicInteractionController : BehaviourController, InteractionFunctionality
    {
        [SerializeField]
        protected InteractionType[] _interactionOnTypes = new InteractionType[] { InteractionType.MouseClick, InteractionType.TouchClick, InteractionType.LookAt };

        [SerializeField]
        protected InteractionState[] _interactionOnStates = new InteractionState[] { InteractionState.OnStart };

        protected bool _interactionIsHover;

        public virtual void Interaction(InteractionState interactionState, InteractionType interactionType)
        {
            if (_interactionOnTypes.Contains(interactionType) == false)
            {
                return;
            }

            if (_interactionOnStates.Contains(interactionState))
            {
                OnInteraction(interactionState, interactionType);

                if (_interactionIsHover == false && interactionState == InteractionState.OnHover)
                {
                    OnHoverBegin(interactionType);

                    _interactionIsHover = true;
                }
            }

            if (_interactionOnStates.Contains(InteractionState.OnHover)
                && interactionState == InteractionState.OnStop)
            {
                OnHoverEnd(interactionType);

                _interactionIsHover = false;
            }
        }

        protected virtual void OnInteraction(InteractionState interactionState, InteractionType interactionType)
        {

        }

        protected virtual void OnHoverBegin(InteractionType interactionType)
        {

        }

        protected virtual void OnHoverEnd(InteractionType interactionType)
        {

        }

        protected override void Awake()
        {

        }

        protected override void Start()
        {

        }

        protected override void Update()
        {

        }
    }
}