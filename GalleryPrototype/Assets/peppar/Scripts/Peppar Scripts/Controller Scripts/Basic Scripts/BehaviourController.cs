using UnityEngine;

namespace peppar
{
    public abstract class BehaviourController : MonoBehaviour
    {
        protected abstract void Start();

        protected abstract void Update();

        protected abstract void Awake();

        protected void HideInInspector(params BehaviourController[] script)
        {
            script.ForEach(s => s.hideFlags = HideFlags.HideInInspector);
        }
    }
}
