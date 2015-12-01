using UnityEngine;
using System.Collections;
using System;

namespace peppar
{
    public class PepparObject
    {
        public readonly Guid ID;

        public readonly string Name;

        public readonly GameObject Object;

        public Transform Transform { get { return Object.transform; } }

        public string Tag { get { return Object.tag; } }

        public PepparObject(string name, GameObject gameObject)
        {
            ID = Guid.NewGuid();

            if (!string.IsNullOrEmpty(name))
                Name = name;
            else
                ErrorMessage("Empty name");

            if (gameObject != null)
                Object = gameObject;
            else
                ErrorMessage("Null object");

            PepparManager.Objects.Add(this);
        }

        protected void ErrorMessage(string _unsupportedType)
        {
            Debug.LogError("PepparObject: " + _unsupportedType + " is not supported or has other problems");
        }
    }
}