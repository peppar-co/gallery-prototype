using UnityEngine;
using System.Collections;
using System;

namespace peppar
{
    [RequireComponent(typeof(MeshRenderer))]
    public class ObjectShadowController : BehaviourController
    {
        [SerializeField]
        private bool _shadowsOnAtStart = true;

        [SerializeField]
        private bool _childShadowsOnAtStart = true;

        protected override void Start()
        {
            var meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.receiveShadows = _shadowsOnAtStart;

            foreach(var childMeshRender in transform.GetComponentsInChildren<MeshRenderer>())
            {
                childMeshRender.receiveShadows = _childShadowsOnAtStart;
            }
        }

        protected override void Update()
        {
        }
    }
}