using System.Collections.Generic;
using UnityEngine;

namespace peppar
{
    public abstract class GameController : BehaviourController
    {
        private Transform _worldCenter;

        public readonly List<Transform> Objects = new List<Transform>();

        public readonly List<Transform> VuforiaObjects = new List<Transform>(); // TODO save all objects in on list as ObjectType with different inheritance for vuforia and other objects

        public Transform WorldCenter
        {
            get
            {
                return _worldCenter;
            }
        }

        protected override void Start()
        {
            _worldCenter = GameObject.FindGameObjectWithTag(Tag.WorldCenter).transform;
            UnityEngine.Assertions.Assert.IsNotNull(_worldCenter, "Singleton: WorldCenter transform is null");

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