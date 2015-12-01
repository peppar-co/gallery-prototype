using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace peppar
{
    public class VuforiaObject : PepparObject
    {
        public readonly List<GameObject> ChildObjects = new List<GameObject>();

        //public Vuforia.ImageTargetBehaviour ImageTargetScript { get; private set; }

        public VuforiaObject(string name, GameObject vuforiaObject, GameObject childObject) : base(name, vuforiaObject)
        {
            AddChildObject(childObject);
        }

        public VuforiaObject(string name, Transform vuforiaTransform, Transform childObject) : base(name, vuforiaTransform.gameObject)
        {
            AddChildObject(childObject);
        }

        public void AddChildObject(GameObject childObject)
        {
            if (childObject == null)
                ErrorMessage("Add Vuforia child object");

            ChildObjects.Add(childObject);
        }

        public void AddChildObject(Transform childObject)
        {
            AddChildObject(childObject.gameObject);
        }

    }
}