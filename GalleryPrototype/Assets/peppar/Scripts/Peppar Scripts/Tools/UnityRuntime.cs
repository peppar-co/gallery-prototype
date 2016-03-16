using UnityEngine;

namespace peppar
{
#if !UNIT_TEST
    public class UnityRuntime : MonoSingleton<UnityRuntime>
    {
        public event System.Action OnGui = delegate { };
        public event System.Action OnGizmos = delegate { };
        public event System.Action OnUpdate = delegate { };
        public event System.Action OnFixedUpdate = delegate { };
        public event System.Action OnQuit = delegate { };
        public event System.Action OnWake = delegate { };

        private void OnGUI()
        {
            OnGui();
        }

        private void OnDrawGizmos()
        {
            if (Camera.current == Camera.main)
            {
                OnGizmos();
            }
        }

        private void Update()
        {
            OnUpdate();
        }

        private void FixedUpdate()
        {
            OnFixedUpdate();
        }

        private void OnApplicationQuit()
        {
            OnQuit();
        }

        private void OnApplicationFocus(bool focusStatus)
        {
            //Debug.Log("OnApplicationFocus: " + focusStatus);
        }

        private void OnApplicationPause(bool isPaused)
        {
            //Debug.Log("OnApplicationPause: " + isPaused);

            if (!isPaused)
            {
                OnWake();
            }
        }
    }
#else
    public class UnityRuntime
    {
        public static UnityRuntime Instance = new UnityRuntime();

        public event System.Action OnGui = delegate { };
        public event System.Action OnGizmos = delegate { };
        public event System.Action OnUpdate = delegate { };
        public event System.Action OnFixedUpdate = delegate { };
        public event System.Action OnQuit = delegate { };
        public event System.Action OnWake = delegate { };

        public UnityEngine.Coroutine StartCoroutine(System.Collections.IEnumerator enumerator) { return null; }
        public void StopCoroutine(UnityEngine.Coroutine routine) { }
    }
#endif
}