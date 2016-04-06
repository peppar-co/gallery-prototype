using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace peppar
{
    public class CharacterComponent : BehaviourController
    {
        [SerializeField]
        private List<GameObject> _hairObjects, _pantsObjects, _shirtObjects;

        [SerializeField]
        private GameObject _faceObject;

        [SerializeField]
        private Transform _hairObjectParent, _pantsObjectParent, _shirtObjectParent;

        [SerializeField]
        private GameObject _hairPlaceholder, _pantsPlaceholder, _shirtPlaceholder;

        [SerializeField]
        private TextMesh _nameTagText;

        [SerializeField]
        private ObjectLookAtController _lookAtController;

        private GameObject _hairObject, _pantsObject, _shirtObject;

        private int _faceIndex, _hairIndex, _pantsIndex, _shirtIndex;

        [SerializeField]
        private Material _defaultFaceMaterial;

        private Material _faceMaterial;

        private string _name;

        public GameObject FaceObject
        {
            get { return _faceObject; }
            set { _faceObject = value; }
        }

        public void SetFace(Texture2D faceTexture)
        {
            _faceMaterial = _defaultFaceMaterial;
            _faceMaterial.mainTexture = faceTexture;
            FaceObject.GetComponent<Renderer>().material = _faceMaterial;
        }

        public void SetName(string name, Camera vufCamera)
        {
            _name = name;
            _nameTagText.text = name;
            _lookAtController.LookAtTarget = vufCamera.transform;
            _lookAtController.enabled = true;

        }

        private void SetHair(int index)
        {
            if (_hairObject != null)
            {
                Destroy(_hairObject);
            }

            _hairObject = Instantiate(_hairObjects[index]);
            _hairObject.transform.SetParent(_hairObjectParent);
            _hairObject.transform.localPosition = Vector3.zero;
            _hairObject.transform.localScale = Vector3.one;
        }

        public void NextHair()
        {
            _hairIndex = NextIndex(_hairIndex, _hairObjects.Count);
            SetHair(_hairIndex);
        }

        public void PreviousHair()
        {
            _hairIndex = PreviousIndex(_hairIndex, _hairObjects.Count);
            SetHair(_hairIndex);
        }

        private void SetPants(int index)
        {
            if (_pantsObject != null)
            {
                Destroy(_pantsObject);
            }

            _pantsObject = Instantiate(_pantsObjects[index]);
            _pantsObject.transform.SetParent(_pantsObjectParent);
            _pantsObject.transform.localPosition = Vector3.zero;
            _pantsObject.transform.localScale = Vector3.one;
        }

        public void NextPants()
        {
            _pantsIndex = NextIndex(_pantsIndex, _pantsObjects.Count);
            SetPants(_pantsIndex);
        }

        public void PreviousPants()
        {
            _pantsIndex = PreviousIndex(_pantsIndex, _pantsObjects.Count);
            SetPants(_pantsIndex);
        }

        private void SetShirt(int index)
        {
            if (_shirtObject != null)
            {
                Destroy(_shirtObject);
            }

            _shirtObject = Instantiate(_shirtObjects[index]);
            _shirtObject.transform.SetParent(_shirtObjectParent);
            _shirtObject.transform.localPosition = Vector3.zero;
            _shirtObject.transform.localScale = Vector3.one;
        }

        public void NextShirt()
        {
            _shirtIndex = NextIndex(_shirtIndex, _shirtObjects.Count);
            SetShirt(_shirtIndex);
        }

        public void PreviousShirt()
        {
            _shirtIndex = PreviousIndex(_shirtIndex, _shirtObjects.Count);
            SetShirt(_shirtIndex);
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
            Destroy(_hairPlaceholder);
            Destroy(_pantsPlaceholder);
            Destroy(_shirtPlaceholder);

            _hairIndex = Random.Range(0, _hairObjects.Count);
            _pantsIndex = Random.Range(0, _pantsObjects.Count);
            _shirtIndex = Random.Range(0, _shirtObjects.Count);

            SetHair(_hairIndex);
            SetPants(_pantsIndex);
            SetShirt(_shirtIndex);
        }

        protected override void Update()
        {

        }
    }
}