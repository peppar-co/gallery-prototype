using UnityEngine;
using System.Collections;
using System;

namespace peppar
{
    public class ObjectCreationController : BehaviourController
    {
        [SerializeField]
        private bool _vuforiaSupport = true;

        [SerializeField]
        private Transform _vuforiaImageTarget;

        public PepparObject CreatePepparObject(Transform newObject, Vector3 position, Vector3 rotation, string name = "")
        {
            Transform createdObject = CreateObject(newObject, position, rotation, name);
            return new PepparObject(name, createdObject.gameObject);
        }

        public PepparObject CreateObject(Transform newObject, Vector3 position, string name = "")
        {
            return CreatePepparObject(newObject, position, Vector3.zero, name);
        }

        public PepparObject CreateObject(Transform newObject, string name = "")
        {
            return CreatePepparObject(newObject, Vector3.zero, Vector3.zero, name);
        }

        #region Vuforia
        public VuforiaObject CreateVuforiaImageObject(string name, Transform newObject = null)
        {
            if (!_vuforiaSupport)
            {
                NotSupportedMessage("Vuforia");
                return null;
            }

            Transform createdVuforiaParent = Instantiate(_vuforiaImageTarget) as Transform;
            createdVuforiaParent.name = name;

            // TODO Add vuforia image setter

            Transform createdObject = AddObjectToVuforiaObject(name, newObject, Vector3.zero, Vector3.zero);

            return new VuforiaObject(name, createdVuforiaParent, createdObject);
        }

        public Transform AddObjectToVuforiaObject(string vuforiaName, Transform newObject, Vector3 position, Vector3 rotation, string name = "")
        {
            if (!_vuforiaSupport)
            {
                NotSupportedMessage("Vuforia");
                return null;
            }

            if (newObject == null)
                return null;

            VuforiaObject vuforiaParent = PepparManager.Objects.Find(o => o.Name.Equals(vuforiaName)) as VuforiaObject;

            if (vuforiaParent == null)
                return null;

            Transform createdChildObject = CreateObject(newObject, position, rotation, name);
            createdChildObject.SetParent(vuforiaParent.Transform);

            vuforiaParent.AddChildObject(createdChildObject);

            return createdChildObject;
        }
        #endregion

        private Transform CreateObject(Transform newTransform, Vector3 position, Vector3 rotation, string name = "")
        {
            Transform createdObject = Instantiate(newTransform, position, Quaternion.Euler(rotation)) as Transform;
            createdObject.name = name;

            return createdObject;
        }

        private void NotSupportedMessage(string _unsupportedType)
        {
            Debug.LogError("ObjectCreationController: " + _unsupportedType + " is not enabled or not supported");
        }

        protected override void Start()
        {
            if (_vuforiaSupport)
                UnityEngine.Assertions.Assert.IsNotNull(_vuforiaImageTarget, "ObjectCreationController: VuforiaImageTarget is null");
        }

        protected override void Update()
        {
        }
    }
}