using UnityEngine;
using System.Collections;
using System;

namespace peppar
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(ObjectPositionController), typeof(ObjectRotationController))]
    public class ObjectTransformController : BehaviourController
    {
        private ObjectPositionController _positionController;
        private ObjectRotationController _rotationController;

        public void SetTo(Vector3 position, Vector3 rotation)
        {
            _positionController.SetToPosition(position);
            _rotationController.SetToRotation(rotation);
        }

        public void SetToTransform(Transform targetTransform)
        {
            _positionController.SetToPosition(targetTransform);
            _rotationController.SetToRotation(targetTransform);
        }

        protected override void Start()
        {

        }

        protected override void Update()
        {

        }
        protected override void Awake()
        {
            _positionController = GetComponent<ObjectPositionController>();
            _rotationController = GetComponent<ObjectRotationController>();

            HideInInspector(_positionController, _rotationController);
        }
    }
}