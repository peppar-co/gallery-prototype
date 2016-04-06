using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

namespace peppar
{
    public class CharacterCreator : BehaviourController
    {
        [SerializeField]
        private Camera _vuforiaCamera, _guiCamera;

        [SerializeField]
        private GameObject _characterPrefab;

        [SerializeField]
        private Transform _creationPosition, _startMovingPosition, _faceSnapshotPosition;

        [SerializeField]
        private Text _nameText;

        private GameObject _currentCharacterObject;

        private CharacterComponent _currentCharacterComponent;

        private CharacterMoveComponent _currentCharacterMoveComponent;

        private bool _allowTakeSnapshot = true, _showPreviewFace = false;

        Texture2D _faceTexture;

        public void StartCharacterCreation()
        {
            SwitchCameras();

            _currentCharacterObject = Instantiate(_characterPrefab);
            _currentCharacterComponent = _currentCharacterObject.GetComponent<CharacterComponent>();
            _currentCharacterMoveComponent = _currentCharacterObject.GetComponent<CharacterMoveComponent>();
            _currentCharacterObject.transform.position = _creationPosition.position;
        }

        public void CancelCharacterCreation()
        {
            SwitchCameras();
            Destroy(_currentCharacterObject);
        }

        public void FinishCharacterCreation()
        {
            SwitchCameras();

            _currentCharacterObject.transform.position = _startMovingPosition.position;
            _currentCharacterObject.transform.SetParent(_startMovingPosition.transform.parent);
            _currentCharacterMoveComponent.Run = true;
            _currentCharacterObject = null;
        }

        private void SwitchCameras()
        {
            _vuforiaCamera.enabled = !_vuforiaCamera.enabled;
            _guiCamera.enabled = !_guiCamera.enabled;
        }

        public void SetFace()
        {
            if (_allowTakeSnapshot)
            {
                StartCoroutine(TakeSnapshot());
            }

            _showPreviewFace = false;
        }

        public void ShowPeviewFace()
        {
            _showPreviewFace = true;
        }

        public void HidePreviewFace()
        {
            _showPreviewFace = false;
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

            if (_allowTakeSnapshot)
            {
                StartCoroutine(TakeSnapshot());
            }
        }

        public void SetName()
        {
            _currentCharacterComponent.SetName(_nameText.text);
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