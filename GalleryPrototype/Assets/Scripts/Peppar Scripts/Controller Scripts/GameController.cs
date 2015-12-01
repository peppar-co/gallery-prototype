using UnityEngine;

namespace peppar
{
    public abstract class GameController : BehaviourController
    {
        private static bool _debug = false;

        public static bool Debug
        {
            get
            {
                return _debug;
            }

            set
            {
                _debug = value;
            }
        }

        protected override void Start()
        {
            OnStart();
        }

        protected override void Update()
        {
            OnUpdate();
        }

        protected abstract void OnStart();

        protected abstract void OnUpdate();
    }
}