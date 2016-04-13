using UnityEngine;

namespace peppar
{
    public class CharacterComponent : BehaviourController
    {
        [SerializeField]
        private GameObject _faceObject;

        [SerializeField]
        private Transform _headObjectParent, _pantsObjectParent, _shirtObjectParent,
            _rightHandObjectParent, _leftHandObjectParent;

        [SerializeField]
        private GameObject _headPlaceholder, _pantsPlaceholder, _shirtPlaceholder,
            _rightHandPlaceholder, _leftHandPlaceholder;

        [SerializeField]
        private TextMesh _nameTagText;

        [SerializeField]
        private ObjectLookAtController _nameLookAtController;

        private GameObject _headObject, _pantsObject, _shirtObject,
            _rightHandObject, _leftHandObject,
            _shirtPictureObject, _rightGadgetPictureObject, _leftGadgetPictureObject;

        [SerializeField]
        private Material _defaultPictureMaterial;

        private Material _faceMaterial, _shirtMaterial, _rightGadgetMaterial, _leftGadgetMaterial;

        private string _name;

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
            if (_headObject != null)
            {
                Destroy(_headObject);
            }

            _headObject = Instantiate(headObject);
            _headObject.transform.SetParent(_headObjectParent);
            _headObject.transform.localPosition = headObject.transform.localPosition;
            _headObject.transform.localScale = headObject.transform.localScale;

            _headObjectParent.GetComponent<InteractionLerpMaterialColorController>().MeshRenderer = _headObject.GetComponent<MeshRenderer>();
        }

        public void SetPants(GameObject pantsObject)
        {
            if (_pantsObject != null)
            {
                Destroy(_pantsObject);
            }

            _pantsObject = Instantiate(pantsObject);
            _pantsObject.transform.SetParent(_pantsObjectParent);
            _pantsObject.transform.localPosition = pantsObject.transform.localPosition;
            _pantsObject.transform.localScale = pantsObject.transform.localScale;

            _pantsObjectParent.GetComponent<InteractionLerpMaterialColorController>().MeshRenderer = _pantsObject.GetComponent<MeshRenderer>();
        }

        public void SetShirt(GameObject shirtObject)
        {
            if (_shirtObject != null)
            {
                Destroy(_shirtObject);
            }

            _shirtObject = Instantiate(shirtObject);
            _shirtObject.transform.SetParent(_shirtObjectParent);
            _shirtObject.transform.localPosition = shirtObject.transform.localPosition; 
            _shirtObject.transform.localScale = shirtObject.transform.localScale;

            _shirtObjectParent.GetComponent<InteractionLerpMaterialColorController>().MeshRenderer = _shirtObject.GetComponent<MeshRenderer>();
        }

        public void SetShirtPicture(Texture shirtTexture)
        {
            _shirtPictureObject = _shirtObject.transform.GetChild(0).gameObject;

            if (_shirtPictureObject == null)
            {
                Debug.LogError("Shirt object: Picture object is missing");
                return;
            }

            _shirtMaterial.mainTexture = Instantiate(shirtTexture);
            _shirtPictureObject.GetComponent<Renderer>().material = _shirtMaterial;
        }

        public void SetRightHand(GameObject rightHandObject, Animator guiAnimator)
        {
            if (_rightHandObject != null)
            {
                Destroy(_rightHandObject);
            }

            _rightHandObject = Instantiate(rightHandObject);
            _rightHandObject.transform.SetParent(_rightHandObjectParent);
            _rightHandObject.transform.localPosition = Vector3.zero;
            _rightHandObject.transform.localScale = rightHandObject.transform.localScale;

            InteractionAnimationController interactionAnimationController = _rightHandObject.GetComponent<InteractionAnimationController>();

            if (interactionAnimationController != null)
            {
                interactionAnimationController.Animator = guiAnimator;
            }

            _rightHandObjectParent.GetComponent<InteractionLerpMaterialColorController>().MeshRenderer = _rightHandObject.GetComponent<MeshRenderer>();
        }

        public void SetLeftHand(GameObject leftHandObject, Animator guiAnimator)
        {
            if (_leftHandObject != null)
            {
                Destroy(_leftHandObject);
            }

            _leftHandObject = Instantiate(leftHandObject);
            _leftHandObject.transform.SetParent(_leftHandObjectParent);
            _leftHandObject.transform.localPosition = Vector3.zero;
            _leftHandObject.transform.localScale = leftHandObject.transform.localScale;

            InteractionAnimationController interactionAnimationController = _leftHandObject.GetComponent<InteractionAnimationController>();

            if (interactionAnimationController != null)
            {
                interactionAnimationController.Animator = guiAnimator;
            }

            _leftHandObjectParent.GetComponent<InteractionLerpMaterialColorController>().MeshRenderer = _leftHandObject.GetComponent<MeshRenderer>();
        }

        public void SetRightGadgetPicture(Texture rightGadgetTexture)
        {
            _rightGadgetPictureObject = _rightHandObject.transform.GetChild(0).GetChild(0).gameObject;

            if (_rightGadgetPictureObject == null)
            {
                Debug.LogError("Right gadget object: Picture object is missing");
                return;
            }

            _rightGadgetMaterial.mainTexture = Instantiate(rightGadgetTexture);
            _rightGadgetPictureObject.GetComponent<Renderer>().material = _rightGadgetMaterial;
        }

        public void SetLeftGadgetPicture(Texture leftGadgetTexture)
        {
            _leftGadgetPictureObject = _leftHandObject.transform.GetChild(0).GetChild(0).gameObject;

            if (_leftGadgetPictureObject == null)
            {
                Debug.LogError("Left gadget object: Picture object is missing");
                return;
            }

            _leftGadgetMaterial.mainTexture = Instantiate(leftGadgetTexture);
            _leftGadgetPictureObject.GetComponent<Renderer>().material = _leftGadgetMaterial;
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

        public void Initialize(Animator guiAnimator)
        {
            _headObjectParent.GetComponent<InteractionAnimationController>().Animator = guiAnimator;
            _pantsObjectParent.GetComponent<InteractionAnimationController>().Animator = guiAnimator;
            _shirtObjectParent.GetComponent<InteractionAnimationController>().Animator = guiAnimator;
            _rightHandObjectParent.GetComponent<InteractionAnimationController>().Animator = guiAnimator;
            _leftHandObjectParent.GetComponent<InteractionAnimationController>().Animator = guiAnimator;
        }

        protected override void Awake()
        {

        }

        protected override void Start()
        {
            Destroy(_headPlaceholder);
            Destroy(_pantsPlaceholder);
            Destroy(_shirtPlaceholder);
            Destroy(_rightHandPlaceholder);
            Destroy(_leftHandPlaceholder);

            _faceMaterial = Instantiate(_defaultPictureMaterial);
            _shirtMaterial = Instantiate(_defaultPictureMaterial);
            _rightGadgetMaterial = Instantiate(_defaultPictureMaterial);
            _leftGadgetMaterial = Instantiate(_defaultPictureMaterial);
        }

        protected override void Update()
        {

        }
    }
}