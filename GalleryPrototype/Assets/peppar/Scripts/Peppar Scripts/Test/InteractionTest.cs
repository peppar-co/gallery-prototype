using UnityEngine;
using System.Collections;
using System;

namespace peppar
{
    public class InteractionTest : BehaviourController, InteractionFunctionality
    {
        public void Interaction(InteractionState state, InteractionType type)
        {
            switch (state)
            {
                case InteractionState.Start:
                    print("start");
                    break;
                case InteractionState.Do:
                    print("do");
                    break;
                case InteractionState.Stop:
                    print("stop");
                    break;
            }
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