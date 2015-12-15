using System;
using UnityEngine;

namespace peppar
{
    public class ObjectPositionController : BehaviourController
    {
        [SerializeField]
        private Transform _startPosition;

        public Vector3 Position
        {
            get
            {
                return transform.position;
            }
            private set
            {
                transform.position = value;
            }
        }

        public void SetToPosition(Vector3 vector)
        {
            Position = vector;
        }

        public void SetToPosition(Transform targetTransformPosition)
        {
            if (targetTransformPosition != null)
                SetToPosition(targetTransformPosition.position);
        }

        public void SetToPosition(float x, float y, float z)
        {
            SetToPosition(new Vector3(x, y, z));
        }

        public void SetToPosition(int x, int y, int z)
        {
            SetToPosition(new Vector3(x, y, z));
        }

        public void SetToPositionStayX(float y, float z)
        {
            SetToPosition(Position.x, y, z);
        }

        public void SetToPositionStayY(float x, float z)
        {
            SetToPosition(x, Position.y, z);
        }

        public void SetToPositionStayZ(float x, float y)
        {
            SetToPosition(x, y, Position.z);
        }

        protected override void Start()
        {
            if (_startPosition != null)
                SetToPosition(_startPosition);
        }

        protected override void Update()
        {

        }
        protected override void Awake()
        {

        }
    }
}