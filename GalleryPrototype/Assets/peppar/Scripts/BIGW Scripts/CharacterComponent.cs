using peppar;
using UnityEngine;
using System.Collections.Generic;

namespace peppar
{
    public class CharacterComponent : BehaviourController
    {
        [SerializeField]
        private List<GameObject> _prefabHeads;

        [SerializeField]
        private List<SkinnedMeshRenderer> _prefabShirts, _prefabPants;

        [SerializeField]
        private GameObject _faceObject;

        [SerializeField]
        private TextMesh _nameTagText;

        [SerializeField]
        private ObjectLookAtController _nameLookAtController;

        private GameObject _headObject, _pantsObject, _shirtObject,
            _rightHandObject, _leftHandObject,
            _shirtPictureObject, _rightGadgetPictureObject, _leftGadgetPictureObject;

        private GameObject _currentHead;
        private SkinnedMeshRenderer _currentPants, _currentShirt;

        [SerializeField]
        private Material _defaultPictureMaterial;

        private Material _faceMaterial, _shirtMaterial, _rightGadgetMaterial, _leftGadgetMaterial;

        private string _name;

        public List<GameObject> PrefabHeads
        {
            get { return _prefabHeads; }
        }

        public List<SkinnedMeshRenderer> PrefabShirts
        {
            get { return _prefabShirts; }
        }

        public List<SkinnedMeshRenderer> PrefabPants
        {
            get { return _prefabPants; }
        }

        public void SetFace(Texture faceTexture)
        {
            _faceMaterial.mainTexture = Instantiate(faceTexture);
            _faceObject.GetComponent<Renderer>().material = _faceMaterial;
        }

        public void SetName(string name, Camera vufCamera)
        {
            _name = name;
            _nameTagText.text = name;
            _nameLookAtController.LookAtTarget = vufCamera.transform;
            _nameLookAtController.enabled = true;
        }

        public void SetHead(GameObject headObject)
        {
            if (_currentHead != null)
            {
                _currentHead.SetActive(false);
            }

            headObject.SetActive(true);
            _currentHead = headObject;
            //if (_headObject != null)
            //{
            //    Destroy(_headObject);
            //}

            //_headObject = Instantiate(headObject);
            //_headObject.transform.SetParent(_headObjectParent);
            //_headObject.transform.localPosition = headObject.transform.localPosition;
            //_headObject.transform.localScale = headObject.transform.localScale;

            //_headObjectParent.GetComponent<InteractionLerpMaterialColorController>().MeshRenderer = _headObject.GetComponent<MeshRenderer>();
        }

        public void SetPants(SkinnedMeshRenderer pantsObject)
        {
            if (_currentPants != null)
            {
                _currentPants.enabled = false;
            }

            pantsObject.enabled = true;
            _currentPants = pantsObject;
        }

        public void SetShirt(SkinnedMeshRenderer shirtObject)
        {
            if (_currentShirt != null)
            {
                _currentShirt.enabled = false;
            }

            shirtObject.enabled = true;
            _currentShirt = shirtObject;
        }

        private int NextIndex(int currentIndex, int count)
        {
            if (currentIndex + 1 >= count)
            {
                return 0;
            }

            return currentIndex + 1;
        }

        private int PreviousIndex(int currentIndex, int count)
        {
            if (currentIndex - 1 < 0)
            {
                return count - 1;
            }

            return currentIndex - 1;
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