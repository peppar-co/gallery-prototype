using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

namespace peppar
{
    public class CharacterCreatorOld : BehaviourController
    {
        [SerializeField]
        private Camera _vuforiaCamera, _guiCamera;

        [SerializeField]
        private GameObject _characterPrefab;

        [SerializeField]
        private Transform _creationPosition, _startMovingPosition, _faceSnapshotPosition;

        [SerializeField]
        private Text _nameInputText;

        private GameObject _currentCharacterObject;

        private CharacterComponentOld _currentCharacterComponent;

        private CharacterMoveComponent _currentCharacterMoveComponent;

        private bool _allowTakeSnapshot = true, _showPreviewFace = false;

        private Texture2D _faceTexture;

        private int _previewFaceFrameCount;

        public void StartCharacterCreation()
        {
            _currentCharacterObject = Instantiate(_characterPrefab);

            _currentCharacterComponent = _currentCharacterObject.GetComponent<CharacterComponentOld>();
            _currentCharacterMoveComponent = _currentCharacterObject.GetComponent<CharacterMoveComponent>();
            _currentCharacterObject.transform.position = _creationPosition.position;
        }

        public void CancelCharacterCreation()
        {
            Destroy(_currentCharacterObject);
        }

        public void FinishCharacterCreation()
        {
            _currentCharacterObject.transform.position = _startMovingPosition.position;
            _currentCharacterObject.transform.SetParent(_startMovingPosition.transform.parent);
            //_currentCharacterMoveComponent.Run = true;
            _currentCharacterObject = null;
        }

        public void SetFace()
        {
            if (_allowTakeSnapshot)
            {
                StartCoroutine(TakeSnapshot());
            }

            _showPreviewFace = false;
        }

        public void ShowPreviewFace()
        {
            _showPreviewFace = true;
            _currentCharacterObject.transform.position = _faceSnapshotPosition.position;
        }

        public void HidePreviewFace()
        {
            _showPreviewFace = false;
            _currentCharacterObject.transform.position = _creationPosition.position;
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

        public void NextHair()
        {
            _currentCharacterComponent.NextHair();
        }

        public void PreviousHair()
        {
            _currentCharacterComponent.PreviousHair();
        }

        public void NextPants()
        {
            _currentCharacterComponent.NextPants();
        }

        public void PreviousPants()
        {
            _currentCharacterComponent.PreviousPants();
        }

        public void NextShirt()
        {
            _currentCharacterComponent.NextShirt();
        }

        public void PreviousShirt()
        {
            _currentCharacterComponent.PreviousShirt();
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