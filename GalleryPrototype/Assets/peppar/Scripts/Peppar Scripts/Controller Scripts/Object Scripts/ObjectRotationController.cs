using UnityEngine;
using System.Collections;

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

        public void SetToRotation(float x, float y, float z)
        {
            SetToRotation(new Vector3(x, y, z));
        }

        public void SetToPosition(int x, int y, int z)
        {
            SetToRotation(new Vector3(x, y, z));
        }

        public void SetToPositionStayX(float y, float z)
        {
            SetToRotation(Rotation.x, y, z);
        }

        public void SetToPositionStayY(float x, float z)
        {
            SetToRotation(x, Rotation.y, z);
        }

        public void SetToPositionStayZ(float x, float y)
        {
            SetToRotation(x, y, Rotation.z);
        }

        protected override void Start()
        {

        }

        protected override void Update()
        {

        }
    }
}