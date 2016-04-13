using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace peppar
{
    public class GenericObjectCreator : BehaviourController
    {
        [Serializable]
        public class GenericObject
        {
            [Serializable]
            public class Module
            {
                [SerializeField]
                private string _moduleName = "New Module";

                [SerializeField]
                private List<GameObject> _modules;

                [SerializeField]
                private List<Texture> _graphics;

                public string ModuleName
                {
                    get
                    {
                        return _moduleName;
                    }
                }

                public void ChangeModule(GenericCreatedObject objectComponent, int index)
                {
                    GameObject newModuleObject = _modules[index];

                    if (newModuleObject == null)
                    {
                        Debug.LogError("ChangeModule: Can not find index -" + index + "- in module list");
                        return;
                    }

                    objectComponent.ChangeModule(ModuleName, newModuleObject);
                }

                public void SetModuleGraphic(GenericCreatedObject objectComponent, int index)
                {
                    Texture graphic = new Texture();

                    if (index < 0)
                    {
                        objectComponent.StartCoroutine(TakeSnapshot(objectComponent));
                    }
                    else
                    {
                        graphic = _graphics[index];

                        if (graphic == null)
                        {
                            Debug.LogError("SetModuleGraphic: Can not find index -" + index + "- in graphics list");
                            return;
                        }

                        objectComponent.SetModuleGraphic(ModuleName, graphic);
                    }
                }

                private bool _allowTakeSnapshot = true;
                private Texture2D _snapshotTexture;
                private int _previewSnapshotFrameCount;

                private IEnumerator TakeSnapshot(GenericCreatedObject objectComponent)
                {
                    _allowTakeSnapshot = false;

                    yield return new WaitForEndOfFrame();

                    _snapshotTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
                    _snapshotTexture.Apply();

                    objectComponent.SetModuleGraphic(ModuleName, _snapshotTexture);

                    _allowTakeSnapshot = true;
                }
            }

            [SerializeField]
            private GameObject _objectPrefab;

            [SerializeField]
            private string _parentName = "Parent Name";

            [SerializeField]
            private List<Module> _modules;

            private GameObject _object;

            private GenericCreatedObject _objectComponent;

            public string ObjectName
            {
                get
                {
                    return _objectPrefab.GetComponent<GenericCreatedObject>().Name;
                }
            }

            public void CreateNewObject()
            {
                _object = Instantiate(_objectPrefab);

                _objectComponent = _object.GetComponent<GenericCreatedObject>();

                var parentObject = GameObject.Find(_parentName).transform;

                if (parentObject != null)
                {
                    _objectPrefab.transform.SetParent(parentObject);
                }
            }

            public void CancelCreation()
            {
                if (_object != null)
                {
                    Destroy(_object);
                }
            }

            public void FinishCreation()
            {
                _object = null;
            }

            public void ChangeModule(string moduleName, int index)
            {
                _modules.Find(m => m.ModuleName == moduleName).ChangeModule(_objectComponent, index);
            }

            public void SetModuleGraphic(string moduleName, int index)
            {
                _modules.Find(m => m.ModuleName == moduleName).SetModuleGraphic(_objectComponent, index);
            }
        }

        [SerializeField]
        private List<GenericObject> _objectPrefabs = new List<GenericObject>();

        private GenericObject _currentGenericObject;

        public void StartCreation(string objectName)
        {
            _currentGenericObject = _objectPrefabs.Find(op => op.ObjectName == objectName);

            if (_currentGenericObject == null)
            {
                Debug.LogError("StartCreation: Can not find  -" + objectName + "- in object prefab list");
                return;
            }

            _currentGenericObject.CreateNewObject();
        }

        public void CancelCurrentCreation()
        {
            if (_currentGenericObject == null)
            {
                return;
            }

            _currentGenericObject.CancelCreation();
        }

        public void FinishCurrentCreation()
        {
            if (_currentGenericObject == null)
            {
                return;
            }

            _currentGenericObject.FinishCreation();
        }

        public void ChangeModule(string moduleNameAndIndex)
        {
            if (_currentGenericObject == null)
            {
                return;
            }

            int index = moduleNameAndIndex[moduleNameAndIndex.Length - 1];

            string moduleName = moduleNameAndIndex.Substring(0, moduleNameAndIndex.Length - 1);

            _currentGenericObject.ChangeModule(moduleName, index);
        }

        public void ChangeModuleGraphic(string moduleNameAndIndex)
        {
            if (_currentGenericObject == null)
            {
                return;
            }

            int index = moduleNameAndIndex[moduleNameAndIndex.Length - 1];

            string moduleName = moduleNameAndIndex.Substring(0, moduleNameAndIndex.Length - 1);

            _currentGenericObject.SetModuleGraphic(moduleName, index);
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