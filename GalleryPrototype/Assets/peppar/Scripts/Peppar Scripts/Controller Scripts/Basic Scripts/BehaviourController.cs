using System.Collections;
using UnityEngine;

namespace peppar
{
    public abstract class BehaviourController : MonoBehaviour
    {
        protected abstract void Start();

        protected abstract void Update();

        protected abstract void Awake();

        protected void ShowInInspector()
        {
            hideFlags = HideFlags.None;
        }

        protected void HideInInspector()
        {
            hideFlags = HideFlags.HideInInspector;
        }

        protected void ShowChildComponentsInInspector(params BehaviourController[] scripts)
        {
#if UNITY_EDITOR
            scripts.ForEach(s => s.ShowInInspector());
#endif
        }

        protected void HideInChildComponentsInspector(params BehaviourController[] scripts)
        {
#if UNITY_EDITOR
            scripts.ForEach(s => s.HideInInspector());
#endif
        }
    }
}
