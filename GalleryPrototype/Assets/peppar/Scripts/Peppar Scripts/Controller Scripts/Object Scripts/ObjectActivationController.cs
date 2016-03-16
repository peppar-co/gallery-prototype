using System;
using UnityEngine;

namespace peppar
{
    public class ObjectActivationController : BehaviourController
    {
        [SerializeField]
        private bool _activeOnStart;

        public bool IsActive { get { return gameObject.activeSelf; } }

        [ContextMenu("Set active")]
        public void SetActive()
        {
            SetActivationStatus(true);
        }

        [ContextMenu("Set inactive")]
        public void SetInactive()
        {
            SetActivationStatus(false);
        }

        [ContextMenu("Change activation status")]
        public void ChangeActivationStatus()
        {
            SetActivationStatus(!IsActive);
        }

        public void SetActivationStatus(bool active)
        {
            gameObject.SetActive(active);
        }

        protected override void Start()
        {
            SetActivationStatus(_activeOnStart);
        }

        protected override void Update()
        {

        }

        protected override void Awake()
        {

        }
    }
}