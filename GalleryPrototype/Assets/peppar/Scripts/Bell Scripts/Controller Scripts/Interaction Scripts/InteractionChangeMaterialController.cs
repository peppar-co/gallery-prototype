using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace peppar.bell
{
    public class InteractionChangeMaterialController : BehaviourController, InteractionFunctionality
    {
        [SerializeField]
        private List<MeshRenderer> _targetMeshes = new List<MeshRenderer>();

        [SerializeField]
        private List<Material> _materials = new List<Material>();

        private int _currentMaterialIndex; 

        public void Interaction(InteractionState interactionState, InteractionType interactionType)
        {
            if(interactionState == InteractionState.Start)
            {

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