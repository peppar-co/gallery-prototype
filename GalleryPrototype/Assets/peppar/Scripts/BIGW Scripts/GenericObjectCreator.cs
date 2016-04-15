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
                private string _moduleID = "New Module";

                [SerializeField]
                private Texture _icon;

                [Serializable]
                public class ModuleObject
                {
                    [SerializeField]
                    private GameObject _model;

                    [SerializeField]
                    private Texture _icon;

                    public GameObject Model
                    {
                        get
                        {
                            return _model;
                        }
                    }
                }

                [SerializeField]
                private List<ModuleObject> _models = new List<ModuleObject>();

                [SerializeField]
                private List<Texture> _graphics = new List<Texture>();

                [SerializeField]
                private List<string> _texts = new List<string>();

                // Snapshot
                private Texture2D _snapshotTexture;
                public bool PreviewPhotoGraphic { get; set; }

                public string ModuleName
                {
                    get
                    {
                        return _moduleID;
                    }
                }

                public Module()
                {
                    _snapshotTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.DXT1Crunched, false);
                }

                public void ChangeModule(GenericCreatedObject objectComponent, int index)
                {
                    GameObject newModuleObject = _models[index].Model;

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

                    PreviewPhotoGraphic = false;
                }

                public void StartPreviewPhotoModuleGraphic(GenericCreatedObject objectComponent)
                {
                    PreviewPhotoGraphic = true;

                    objectComponent.StartCoroutine(PreviewSnapshot(objectComponent));
                }

                public void SetModuleText(GenericCreatedObject objectComponent, int index)
                {
                    string text = string.Empty;

                    if (index < 0)
                    {
                        // TODO
                    }
                    else
                    {
                        text = _texts[index];

                        if (string.IsNullOrEmpty(text))
                        {
                            Debug.LogError("SetModuleText: Can not find index -" + index + "- in text list");
                            return;
                        }

                        objectComponent.SetModuleText(ModuleName, text);
                    }
                }

                private IEnumerator TakeSnapshot(GenericCreatedObject objectComponent)
                {
                    yield return new WaitForEndOfFrame();

                    _snapshotTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
                    _snapshotTexture.Apply();

                    objectComponent.SetModuleGraphic(ModuleName, _snapshotTexture);
                }

                private IEnumerator PreviewSnapshot(GenericCreatedObject objectComponent)
                {
                    while (PreviewPhotoGraphic)
                    {
                        objectComponent.StartCoroutine(TakeSnapshot(objectComponent));

                        yield return new WaitForSeconds(0.5f);
                    }
                }
            }

            [SerializeField]
            private GameObject _objectPrefab;

            [SerializeField]
            private string _parentName = "Parent Name";

            [SerializeField]
            private List<Module> _modules = new List<Module>();

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
                GetModuleByName(moduleName).ChangeModule(_objectComponent, index);
            }

            public void SetModuleGraphic(string moduleName, int index)
            {
                GetModuleByName(moduleName).SetModuleGraphic(_objectComponent, index);
            }

            public void StartPreviewPhotoModuleGraphic(string moduleName)
            {
                GetModuleByName(moduleName).StartPreviewPhotoModuleGraphic(_objectComponent);
            }

            public void StopPreviewPhotoModuleGraphic(string moduleName)
            {
                GetModuleByName(moduleName).PreviewPhotoGraphic = false;
            }

            public void SetModuleText(string moduleName, int index)
            {
                _modules.Find(m => m.ModuleName == moduleName).SetModuleText(_objectComponent, index);
            }

            private Module GetModuleByName(string moduleName)
            {
                return _modules.Find(m => m.ModuleName == moduleName);
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

        public void ShowPhotoPreviewOnModule(string moduleName)
        {
            if (_currentGenericObject == null)
            {
                return;
            }

            _currentGenericObject.StartPreviewPhotoModuleGraphic(moduleName);
        }

        public void StopPhotoPreviewObNodule(string moduleName)
        {
            if (_currentGenericObject == null)
            {
                return;
            }

            _currentGenericObject.StopPreviewPhotoModuleGraphic(moduleName);
        }

        // TODO ChangeModuleText

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