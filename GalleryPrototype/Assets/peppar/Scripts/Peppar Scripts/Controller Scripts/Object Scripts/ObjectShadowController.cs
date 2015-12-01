using UnityEngine;
using System.Collections;
using System;

namespace peppar
{
    [RequireComponent(typeof(MeshRenderer))]
    public class ObjectShadowController : BehaviourController
    {
        [SerializeField]
        private bool _recieveShadowsAtStart = true;

        [SerializeField]
        private UnityEngine.Rendering.ShadowCastingMode _castShadowModeAtStart = UnityEngine.Rendering.ShadowCastingMode.On;

        [SerializeField]
        private bool _childRecieveShadowsAtStart = true;

        [SerializeField]
        private UnityEngine.Rendering.ShadowCastingMode _childCastShadowModeAtStart = UnityEngine.Rendering.ShadowCastingMode.On;

        protected override void Start()
        {
            var meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.receiveShadows = _recieveShadowsAtStart;
            meshRenderer.shadowCastingMode = _castShadowModeAtStart;

            foreach(var childMeshRender in transform.GetComponentsInChildren<MeshRenderer>())
            {
                childMeshRender.receiveShadows = _childRecieveShadowsAtStart;
                childMeshRender.shadowCastingMode = _childCastShadowModeAtStart;
            }
        }

        protected override void Update()
        {
        }
    }
}