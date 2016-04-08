using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

namespace peppar
{
    public class CharacterCreator : BehaviourController
    {
        [SerializeField]
        private List<GameObject> _headObjects, _pantsObjects, _shirtObjects, _handObjects;

        [SerializeField]
        private List<Texture> _faceTextures, _shirtTextures, _gadgetTextures;

        [SerializeField]
        private Camera _vuforiaCamera, _guiCamera;

        [SerializeField]
        private Animator _guiAnimator;

        [SerializeField]
        private GameObject _characterPrefab;

        [SerializeField]
        private Transform _creationPosition, _startMovingPosition; // _faceSnapshotPosition;

        [SerializeField]
        private Text _nameInputText, _shirtInputText, _rightGadgetInputText, _leftGadgetInputText;

        // Character components
        private GameObject _currentCharacterObject;
        private CharacterComponent _currentCharacterComponent;
        private CharacterMoveComponent _currentCharacterMoveComponent;

        // Face snapshot
        private bool _allowTakeSnapshot = true, _showPreviewFace = false;
        private Texture2D _faceTexture;
        private int _previewFaceFrameCount;

        public void StartCharacterCreation()
        {
            _currentCharacterObject = Instantiate(_characterPrefab);

            _currentCharacterComponent = _currentCharacterObject.GetComponent<CharacterComponent>();
            _currentCharacterMoveComponent = _currentCharacterObject.GetComponent<CharacterMoveComponent>();

            _currentCharacterComponent.Initialize(_guiAnimator);

            _currentCharacterObject.transform.position = _creationPosition.position;
            _currentCharacterObject.transform.localScale = _creationPosition.localScale;

            // Random character
            int headIndex = Random.Range(0, _headObjects.Count);
            int pantsIndex = Random.Range(0, _pantsObjects.Count);
            int shirtIndex = Random.Range(0, _shirtObjects.Count);
            int rightHandIndex = Random.Range(0, _handObjects.Count);
            int leftHandIndex = Random.Range(0, _handObjects.Count);
            //int rightGadgetIndex = Random.Range(0, _shirtObjects.Count);
            //int leftGadgetIndex = Random.Range(0, _shirtObjects.Count);

            SetHead(headIndex);
            SetPants(pantsIndex);
            SetShirt(shirtIndex);
            SetRightHand(rightHandIndex);
            SetLeftHand(leftHandIndex);
        }

        public void CancelCharacterCreation()
        {
            Destroy(_currentCharacterObject);
        }

        public void FinishCharacterCreation()
        {
            _currentCharacterObject.transform.position = _startMovingPosition.position;
            _currentCharacterObject.transform.localScale = _startMovingPosition.localScale;
            _currentCharacterObject.transform.SetParent(_startMovingPosition.transform.parent);
            _currentCharacterMoveComponent.Run = true;
            _currentCharacterObject = null;
        }

        public void SetFace(int index = -1)
        {
            if (index < 0 && _allowTakeSnapshot)
            {
                StartCoroutine(TakeSnapshot());
            }
            else if (index >= 0)
            {
                _currentCharacterComponent.SetFace(_faceTextures[index]);
            }

            _showPreviewFace = false;
        }

        public void ShowPreviewFace()
        {
            _showPreviewFace = true;
            //_currentCharacterObject.transform.position = _faceSnapshotPosition.position;
        }

        public void HidePreviewFace()
        {
            _showPreviewFace = false;
            //_currentCharacterObject.transform.position = _creationPosition.position;
        }

        private IEnumerator TakeSnapshot()
        {
            _allowTakeSnapshot = false;

            yield return new WaitForEndOfFrame();

            _faceTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            _faceTexture.Apply();

            _currentCharacterComponent.SetFace(_faceTexture);

            _allowTakeSnapshot = true;
        }

        private void ShowFaceInPreview()
        {
            if (_showPreviewFace == false || _currentCharacterObject == null)
            {
                return;
            }

            _previewFaceFrameCount++;

            if (_allowTakeSnapshot && _previewFaceFrameCount > 4)
            {
                StartCoroutine(TakeSnapshot());
                _previewFaceFrameCount = 0;
            }
        }

        public void SetName()
        {
            _currentCharacterComponent.SetName(_nameInputText.text, _vuforiaCamera);
        }

        public void SetHead(int index)
        {
            _currentCharacterComponent.SetHead(_headObjects[index]);
        }

        public void SetPants(int index)
        {
            _currentCharacterComponent.SetPants(_pantsObjects[index]);
        }

        public void SetShirt(int index)
        {
            _currentCharacterComponent.SetShirt(_shirtObjects[index]);
        }

        public void SetShirtPicture(int index)
        {

        }

        public void SetShirtText(int index)
        {

        }

        public void SetRightHand(int index)
        {
            _currentCharacterComponent.SetRightHand(_handObjects[index], _guiAnimator);
        }

        public void SetLeftHand(int index)
        {
            _currentCharacterComponent.SetLeftHand(_handObjects[index], _guiAnimator);
        }

        protected override void Awake()
        {

        }

        protected override void Start()
        {
            _faceTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, true);
        }

        protected override void Update()
        {
            ShowFaceInPreview();
        }
    }
}