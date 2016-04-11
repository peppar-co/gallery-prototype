using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace peppar
{
    public class GenericCreatedObject : BehaviourController
    {
        [Serializable]
        public class GenericCreatedComponent
        {
            [SerializeField]
            private string _componentName;

            [SerializeField]
            private Transform _componentParent;

            [SerializeField]
            private Transform _componentPlaceholder;

            private GameObject _componentObject;

            public void CreateComponent(GameObject newComponentObject)
            {
                if(_componentObject != null)
                {
                    Destroy(_componentObject);
                }

                _componentObject = Instantiate(newComponentObject);
                _componentObject.transform.SetParent(_componentParent);
                _componentObject.transform.localPosition = Vector3.zero;
                _componentObject.transform.localScale = Vector3.one;

                _componentParent.GetComponent<InteractionLerpMaterialColorController>().MeshRenderer = _componentObject.GetComponent<MeshRenderer>();
            }
        }

        [SerializeField]
        private List<GenericCreatedComponent> _genericComponents = new List<GenericCreatedComponent>();



        protected override void Awake()
        {

        }

        protected override void Start()
        {

        }

        protected override void Update()
        {

        }
    }
}