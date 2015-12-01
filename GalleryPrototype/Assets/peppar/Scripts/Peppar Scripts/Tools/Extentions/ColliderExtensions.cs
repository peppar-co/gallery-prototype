using UnityEngine;

namespace peppar
{
    public static class ColliderExtensions
    {
        private static readonly Vector3[] _cubeNormals = { Vector3.forward, Vector3.right, Vector3.up, Vector3.back, Vector3.left, Vector3.down };

        public static Vector3 ClosestPointOnSurface(this Collider instance, Vector3 point)
        {
            if (instance is BoxCollider)
            {
                return ((BoxCollider)instance).ClosestPointOnSurface(point);
            }
            else if (instance is SphereCollider)
            {
                return ((SphereCollider)instance).ClosestPointOnSurface(point);
            }
            else
            {
                return instance.ClosestPointOnBounds(point);
            }
        }

        public static Vector3 ClosestPointOnSurface(this SphereCollider instance, Vector3 point)
        {
            Vector3 center = instance.transform.TransformPoint(instance.center);
            Vector3 normal = (point - center).normalized;
            return center + normal * instance.radius;
        }

        public static bool ContainsPoint(this Collider instance, Vector3 point)
        {
            if (instance is BoxCollider)
            {
                return ((BoxCollider)instance).ContainsPoint(point);
            }
            else if (instance is SphereCollider)
            {
                return ((SphereCollider)instance).ContainsPoint(point);
            }
            else
            {
                return instance.bounds.Contains(point);
            }
        }

        public static bool ContainsPoint(this BoxCollider instance, Vector3 point)
        {
            return instance.DistanceToSurface(point) < 0.0f;
        }

        public static Vector3 ClosestPointOnSurface(this BoxCollider instance, Vector3 point)
        {
            point = instance.transform.InverseTransformPoint(point) - instance.center;

            Vector3 min = new Vector3(-instance.size.x, -instance.size.y, -instance.size.z);
            Vector3 max = new Vector3(instance.size.x, instance.size.y, instance.size.z);

            float minDistance = float.MaxValue;
            Vector3 closestPlaneNormal = Vector3.zero;
            Vector3 closestPoint = Vector3.zero;
            for (int i = 0; i < _cubeNormals.Length; ++i)
            {
                Vector3 planeNormal = _cubeNormals[i];
                Vector3 planePoint = max;
                if (i > 2)
                {
                    planePoint = min;
                }

                float distance = Mathf.Abs(Vector3.Dot(point, planeNormal) - Vector3.Dot(planePoint, planeNormal));
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestPlaneNormal = planeNormal;
                }
            }

            closestPoint = point + closestPlaneNormal * (-minDistance);
            closestPoint = instance.transform.TransformPoint(closestPoint);

            return closestPoint;
        }

        public static bool ContainsPoint(this SphereCollider instance, Vector3 point)
        {
            Vector3 center = instance.transform.TransformPoint(instance.center);
            return (point - center).magnitude <= instance.radius;
        }

        public static Vector3 Center(this Collider instance)
        {
            if (instance is BoxCollider)
            {
                return ((BoxCollider)instance).Center();
            }
            else if (instance is SphereCollider)
            {
                return ((SphereCollider)instance).Center();
            }
            else
            {
                return instance.transform.position;
            }
        }

        public static Vector3 Center(this SphereCollider instance)
        {
            return instance.transform.TransformPoint(instance.center);
        }

        public static Vector3 Center(this BoxCollider instance)
        {
            return instance.transform.TransformPoint(instance.center);
        }

        public static float DistanceToSurface(this SphereCollider instance, Vector3 point)
        {
            Vector3 center = instance.transform.TransformPoint(instance.center);
            return (point - center).magnitude - instance.radius;
        }

        public static float DistanceToSurface(this BoxCollider instance, Vector3 point)
        {
            point = instance.transform.InverseTransformPoint(point) - instance.center;

            Vector3 max = new Vector3(instance.size.x / 2.0f, instance.size.y / 2.0f, instance.size.z / 2.0f);
            Vector3 direction = new Vector3(Mathf.Abs(point.x), Mathf.Abs(point.y), Mathf.Abs(point.z)) - max;

            return Mathf.Min(Mathf.Max(direction.x, Mathf.Max(direction.y, direction.z)), 0.0f) + Vector3.Max(direction, Vector3.zero).magnitude;
        }

        public static float DistanceToSurface(this Collider instance, Vector3 point)
        {
            if (instance is BoxCollider)
            {
                return ((BoxCollider)instance).DistanceToSurface(point);
            }
            else if (instance is SphereCollider)
            {
                return ((SphereCollider)instance).DistanceToSurface(point);
            }
            else
            {
                return (instance.bounds.ClosestPoint(point) - point).magnitude;
            }
        }
    }
}
