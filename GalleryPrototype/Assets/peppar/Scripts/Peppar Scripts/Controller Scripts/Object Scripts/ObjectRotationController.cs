using UnityEngine;
using System.Collections;
using System;

namespace peppar
{
    public class ObjectRotationController : BehaviourController
    {
        public Vector3 Rotation
        {
            get
            {
                return transform.rotation.eulerAngles;
            }
            private set
            {
                transform.rotation = Quaternion.Euler(value);
            }
        }

        public void SetToRotation(Vector3 vector)
        {
            Rotation = vector;
        }

        public void SetToRotation(Transform targetTransformRotation)
        {
            if (targetTransformRotation != null)
                SetToRotation(targetTransformRotation.rotation.eulerAngles);
        }

        public void SetToRotation(float x, float y, float z)
        {
            SetToRotation(new Vector3(x, y, z));
        }

        public void SetToRotation(int x, int y, int z)
        {
            SetToRotation(new Vector3(x, y, z));
        }

        public void SetToRotationStayX(float y, float z)
        {
            SetToRotation(Rotation.x, y, z);
        }

        public void SetToRotationStayY(float x, float z)
        {
            SetToRotation(x, Rotation.y, z);
        }

        public void SetToRotationStayZ(float x, float y)
        {
            SetToRotation(x, y, Rotation.z);
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