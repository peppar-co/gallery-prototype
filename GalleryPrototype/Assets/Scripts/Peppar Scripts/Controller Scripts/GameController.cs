using System.Collections.Generic;
using UnityEngine;

namespace peppar
{
    public class GameController : BehaviourController
    {
        private Transform _worldCenter;

        public readonly List<Transform> Objects = new List<Transform>();

        public readonly List<Transform> VuforiaObjects = new List<Transform>();

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
        }

        protected override void Update()
        {

        }
    }
}