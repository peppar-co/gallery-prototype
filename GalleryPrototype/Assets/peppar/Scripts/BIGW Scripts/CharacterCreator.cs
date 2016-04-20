using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

namespace peppar
{
    public class CharacterCreator : BehaviourController
    {
        [SerializeField]
        private GameObject _creationObject, _worldObject, _characterObject;

        private List<GameObject> _headObjects;
        private List<SkinnedMeshRenderer> _pantsObjects, _shirtObjects;

        [SerializeField]
        private List<Texture> _faceTextures;

        [SerializeField]
        private Camera _vuforiaCamera, _guiCamera;

        [SerializeField]
        private Animator _guiAnimator;

        [SerializeField]
        private GameObject _characterScenePrefab;

        [SerializeField]
        private Transform _creationPosition, _startMovingPosition;     //, _snapshotPosition;

        [SerializeField]
        private Text _nameInputText;

        // Character components
        private GameObject _currentCharacterObject;
        private CharacterComponent _currentCharacterComponent;
        private CharacterMoveComponent _currentCharacterMoveComponent;

        private bool _showPreviewFace = false, _showPreviewShirt = false,
            _showPreviewRightGadget = false, _showPreviewLeftGadget = false;

        // Snapshot
        private bool _allowTakeSnapshot = true;
        private Texture2D _snapshotTexture;
        private int _previewSnapshotFrameCount;

        private enum SnapshotTexture
        {
            Face,
            Shirt,
            RightGadget,
            LeftGadget
        }

        public void StartCharacterCreation()
        {
            _currentCharacterObject = Instantiate(_characterScenePrefab);
            _currentCharacterObject.transform.SetParent(_creationPosition.transform.parent);

            _currentCharacterComponent = _currentCharacterObject.GetComponent<CharacterComponent>();
            _currentCharacterMoveComponent = _currentCharacterObject.GetComponent<CharacterMoveComponent>();

            _currentCharacterObject.transform.position = _creationPosition.position;
            _currentCharacterObject.transform.localScale = _creationPosition.localScale;
            _currentCharacterObject.SetActive(true);

            _headObjects = _currentCharacterComponent.PrefabHeads;
            _shirtObjects = _currentCharacterComponent.PrefabShirts;
            _pantsObjects = _currentCharacterComponent.PrefabPants;

            // Random character
            int headIndex = Random.Range(0, _headObjects.Count);
            int pantsIndex = Random.Range(0, _pantsObjects.Count);
            int shirtIndex = Random.Range(0, _shirtObjects.Count);

            SetHead(headIndex);
            SetPants(pantsIndex);
            SetShirt(shirtIndex);

            _worldObject.SetActive(false);
            _characterObject.SetActive(false);
            _creationObject.SetActive(true);
        }

        public void CancelCharacterCreation()
        {
            Destroy(_currentCharacterObject);

            _worldObject.SetActive(true);
            _characterObject.SetActive(true);
            _creationObject.SetActive(false);
        }

        public void FinishCharacterCreation()
        {
            Destroy(_currentCharacterComponent);
            _currentCharacterObject.transform.SetParent(_startMovingPosition.transform.parent);
            _currentCharacterObject.transform.position = _startMovingPosition.position;
            _currentCharacterObject.transform.localScale = _startMovingPosition.localScale;
            _currentCharacterObject = null;

            _worldObject.SetActive(true);
            _characterObject.SetActive(true);
            _creationObject.SetActive(false);
        }

        public void SetFace(int index = -1)
        {
            if (index < 0 && _allowTakeSnapshot)
            {
                StartCoroutine(TakeSnapshot(SnapshotTexture.Face));
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

        private IEnumerator TakeSnapshot(SnapshotTexture snapshotTexture)
        {
            _allowTakeSnapshot = false;

            yield return new WaitForEndOfFrame();

            _snapshotTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            _snapshotTexture.Apply();

            switch (snapshotTexture)
            {
                case SnapshotTexture.Face:
                    _currentCharacterComponent.SetFace(_snapshotTexture);
                    break;
            }

            _allowTakeSnapshot = true;
        }

        private void ShowFaceInPreview()
        {
            if (_showPreviewFace == false || _currentCharacterObject == null)
            {
                return;
            }

            _previewSnapshotFrameCount++;

            if (_allowTakeSnapshot && _previewSnapshotFrameCount > 4)
            {
                StartCoroutine(TakeSnapshot(SnapshotTexture.Face));
                _previewSnapshotFrameCount = 0;
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

        protected override void Awake()
        {

        }

        protected override void Start()
        {
            _snapshotTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, true);
        }

        protected override void Update()
        {
            ShowFaceInPreview();
        }
    }
}