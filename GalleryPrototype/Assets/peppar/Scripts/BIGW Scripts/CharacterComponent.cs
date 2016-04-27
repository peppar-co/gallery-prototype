using peppar;
using UnityEngine;
using System.Collections.Generic;

namespace peppar
{
    public class CharacterComponent : BehaviourController
    {
        [SerializeField]
        private SkinnedMeshRenderer _bodyFace;

        [SerializeField]
        private List<GameObject> _prefabHeads;

        [SerializeField]
        private List<SkinnedMeshRenderer> _prefabShirts, _prefabPants;

        [SerializeField]
        private List<Material> _faceMaterials = new List<Material>();

        [SerializeField]
        private TextMesh _nameTagText;

        [SerializeField]
        private ObjectLookAtController _nameLookAtController;

        private GameObject _headObject, _pantsObject, _shirtObject;

        private GameObject _currentHead;
        private SkinnedMeshRenderer _currentPants, _currentShirt;

        [SerializeField]
        private Material _defaultPictureMaterial;

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

        public List<Material> FaceMaterials
        {
            get { return _faceMaterials; }
        }

        public void SetFace(Material faceMaterial)
        {
            _bodyFace.material = Instantiate(faceMaterial);
        }

        public void SetName(string name, Camera vufCamera)
        {
            _name = name;
            if (_nameTagText != null)
            {
                _nameTagText.text = name;
            }
            if (_nameLookAtController != null)
            {
                _nameLookAtController.LookAtTarget = vufCamera.transform;
                _nameLookAtController.enabled = true;
            }
        }

        public void SetHead(GameObject headObject)
        {
            if (_currentHead != null)
            {
                _currentHead.SetActive(false);
            }

            headObject.SetActive(true);
            _currentHead = headObject;
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