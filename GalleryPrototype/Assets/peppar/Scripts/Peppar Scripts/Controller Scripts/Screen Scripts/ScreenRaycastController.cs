﻿using UnityEngine;

namespace peppar
{
    public class ScreenRaycastController : BehaviourController
    {
        [SerializeField]
        private Camera _camera = Camera.main;

        [SerializeField]
        private LayerMask _ignoredLayer;

        public Transform FirstObjectAtScreenCenter
        {
            get { return GetFirstObjectAtScreenPos(new Vector2(0.5f, 0.5f)); }
        }

        public RaycastHit GetFirstHitAtScreenPos(Vector2 screenPosition)
        {
            Ray ray = _camera.ViewportPointToRay(new Vector3(screenPosition.x, screenPosition.y));
            RaycastHit hit;

            Physics.Raycast(ray, out hit);

            return hit;
        }

        public Transform GetFirstObjectAtScreenPos(Vector2 screenPosition)
        {
            return GetFirstHitAtScreenPos(screenPosition).transform;
        }

        public Vector3 GetFirstHitPosAtScreenPos(Vector2 screenPosition)
        {
            return GetFirstHitAtScreenPos(screenPosition).point;
        }

        public Transform GetFirtsObjectAtScreenPosPixel(Vector2 screenPixelPosition)
        {
            return GetFirstObjectAtScreenPos(GetScreenPosFromScreenPixelPos(screenPixelPosition));
        }

        public Vector3 GetScreenPosFromScreenPixelPos(Vector3 screenPixelPosition)
        {
            return _camera.ViewportToScreenPoint(screenPixelPosition);
        }

        public Vector2 GetScreenPosFromScreenPixelPos(Vector2 screenPixelPosition)
        {
            Vector3 screenPosition = _camera.ViewportToScreenPoint(GetScreenPosFromScreenPixelPos(
                new Vector3(screenPixelPosition.x, screenPixelPosition.y, 0)));

            return new Vector2(screenPosition.x, screenPosition.y);
        }

        protected override void Start()
        {
            UnityEngine.Assertions.Assert.IsNotNull(_camera, "ScreenRaycastController: Camera is null");
        }

        protected override void Update()
        {
        }
    }
}