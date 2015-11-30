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

        public Transform CreateObject(Transform newTransform, Vector3 position, Vector3 rotation, string name = "")
        {
            Transform createdObject = Instantiate(newTransform, position, Quaternion.Euler(rotation)) as Transform;
            createdObject.name = name;

            Singleton.GameManager.GameController.Objects.Add(createdObject);

            return createdObject;
        }

        public Transform CreateObject(Transform newTransform, Vector3 position, string name = "")
        {
            return CreateObject(newTransform, position, Vector3.zero, name);
        }

        public Transform CreateObject(Transform newTransform, string name = "")
        {
            return CreateObject(newTransform, Vector3.zero, Vector3.zero, name);
        }

        #region Vuforia
        public Transform CreateVuforiaObjectImage(string name, Transform newTransform = null)
        {
            if (!_vuforiaSupport)
            {
                NotSupportedMessage("Vuforia");
                return null;
            }

            if (Singleton.GameManager.GameController.VuforiaObjects.Exists(o => o.name.Equals(name)))
                return null;

            Transform createdVuforiaParent = Instantiate(_vuforiaImageTarget) as Transform;
            createdVuforiaParent.name = name;

            // TODO Add vuforia image setter

            Singleton.GameManager.GameController.VuforiaObjects.Add(createdVuforiaParent);

            Transform createdObject = AddObjectToVuforiaObject(name, newTransform, Vector3.zero, Vector3.zero);

            return createdVuforiaParent;
        }

        public Transform AddObjectToVuforiaObject(string vuforiaName, Transform newTransform, Vector3 position, Vector3 rotation, string name = "")
        {
            if (!_vuforiaSupport)
            {
                NotSupportedMessage("Vuforia");
                return null;
            }

            if (newTransform == null)
                return null;

            Transform vuforiaParent = Singleton.GameManager.GameController.VuforiaObjects.Find(o => o.name.Equals(vuforiaName));

            if (vuforiaParent == null)
                return null;

            Transform createdObject = CreateObject(newTransform, position, rotation, name);
            createdObject.SetParent(vuforiaParent);

            return createdObject;
        }
        #endregion

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