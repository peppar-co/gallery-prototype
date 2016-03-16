using UnityEngine;

namespace peppar
{
    public static class GameObjectExtensions
    {
        public static T GetOrAddComponent<T>(this GameObject instance) where T : Component
        {
            return instance.GetComponent<T>() != null ? instance.GetComponent<T>() : instance.AddComponent<T>();
        }

        public static T GetOrAddComponent<T>(this Transform instance) where T : Component
        {
            return instance.GetComponent<T>() != null ? instance.GetComponent<T>() : instance.gameObject.AddComponent<T>();
        }

        public static void SetLayerRecursively(this GameObject instance, int layer)
        {
            var children = instance.GetComponentsInChildren<Transform>();
            foreach (var c in children)
            {
                c.gameObject.layer = layer;
            }
        }
    }
}
