using UnityEngine;
using System.Collections;
using System;

namespace peppar
{
    public class ObjectScreenController : BehaviourController
    {
        [SerializeField]
        private Camera _camera = Camera.main;

        public bool IsInVision;

        public Vector3 PositionOnScreenPixel3D
        {
            get
            {
                return _camera.WorldToScreenPoint(transform.position);
            }
        }

        public Vector2 PositionOnScreenPixel
        {
            get
            {
                Vector3 positionOnScreen = PositionOnScreenPixel3D;

                return new Vector2(positionOnScreen.x, positionOnScreen.y);
            }
        }

        public Vector3 PositionOnScreen3D
        {
            get
            {
                return _camera.WorldToViewportPoint(transform.position);
            }
        }

        public Vector2 PositionOnScreen
        {
            get
            {
                Vector3 positionOnScreen = PositionOnScreen3D;

                return new Vector2(positionOnScreen.x, positionOnScreen.y);
            }
        }

        public float DistanceToCamera
        {
            get { return PositionOnScreen3D.z; }
        }



        protected override void Start()
        {
        }

        protected override void Update()
        {
        }
    }
}