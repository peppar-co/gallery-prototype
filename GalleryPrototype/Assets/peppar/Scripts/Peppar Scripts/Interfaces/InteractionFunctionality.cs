using UnityEngine;
using System.Collections;

namespace peppar
{
    public interface InteractionFunctionality
    {
        void Interaction(InteractionState interactionState, InteractionType interactionType);
    }
}