using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace peppar
{
    public class GenericCreatedObject : BehaviourController
    {
        [Serializable]
        public class GenericCreatedModule
        {
            [SerializeField]
            private string _moduleName = "New Module";

            [SerializeField]
            private Transform _moduleParent;

            [SerializeField]
            private Transform _modulePlaceholder;

            private Transform _moduleObject, _moduleGraphicObject;

            private Material _moduleGraphicMaterial;

            public string ModuleName
            {
                get
                {
                    return _moduleName;
                }
            }

            public void ChangeModule(GameObject newModuleObject)
            {
                if (_moduleObject != null)
                {
                    Destroy(_moduleObject);
                }

                _moduleObject = Instantiate(newModuleObject).transform;
                _moduleObject.SetParent(_moduleParent);
                _moduleObject.localPosition = Vector3.zero;
                _moduleObject.localScale = Vector3.one;

                _moduleParent.GetComponent<InteractionLerpMaterialColorController>().MeshRenderer = _moduleObject.GetComponent<MeshRenderer>();
            }

            public void SetModuleGraphic(Texture moduleTexture)
            {
                if (_moduleGraphicMaterial == null)
                {
                    return;
                }

                _moduleGraphicObject = _moduleObject.transform.FindChild("Graphic Module");

                if (_moduleGraphicObject == null)
                {
                    Debug.LogError("GenericCreatedModule: Graphic object is missing (child with name \"Graphic Module\" is needed)");
                    return;
                }

                _moduleGraphicMaterial.mainTexture = Instantiate(moduleTexture);
                _moduleGraphicObject.GetComponent<Renderer>().material = _moduleGraphicMaterial;
            }
        }

        [SerializeField]
        private string _name = "New Object";

        [SerializeField]
        private List<GenericCreatedModule> _genericModules = new List<GenericCreatedModule>();

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public void ChangeModule(string moduleName, GameObject newModuleObject)
        {
            var genericModule = GetGenericModule(moduleName);

            if (genericModule == null)
            {
                return;
            }

            genericModule.ChangeModule(newModuleObject);
        }

        public void SetModuleGraphic(string moduleName, Texture moduleTexture)
        {
            var genericModule = GetGenericModule(moduleName);

            if(genericModule == null)
            {
                return;
            }

            genericModule.SetModuleGraphic(moduleTexture);
        }

        public GenericCreatedModule GetGenericModule(string moduleName)
        {
            return _genericModules.Find(gm => gm.ModuleName == moduleName);

        }

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