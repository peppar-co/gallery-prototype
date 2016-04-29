using UnityEngine;
using System.Collections;
using System.Linq;

namespace peppar
{
    public class PeppComponent : BehaviourController, InteractionFunctionality
    {
        [SerializeField]
        private InteractionType[] _interactionOnTypes = new InteractionType[] { InteractionType.MouseClick, InteractionType.TouchClick };

        [SerializeField]
        private InteractionState _peppOnState = InteractionState.OnStart;

        [SerializeField]
        private string _peppId = "New Pepp";

        [SerializeField]
        private PeppObjectController _peppObject;

        [SerializeField]
        private GameObject[] _peppItems = new GameObject[2];

        [SerializeField]
        private GameObject _peppGUI;

        [SerializeField]
        private GameObject _worldObject, _characterObject;

        [SerializeField]
        private Transform _peppBuildingTransform;

        [SerializeField]
        private PeppController _peppController;

        [SerializeField]
        private BIGWGUIController _guiController;

        private bool _active;

        private Transform _initialParent;

        private Vector3 _initialPosition, _initialScale;

        private GameObject _shopItem1, _shopItem2;

        private Quaternion _initialRotation;

        public string PeppId
        {
            get
            {
                return _peppId;
            }
        }

        public int TaskIndex { get; set; }

        public void Interaction(InteractionState interactionState, InteractionType interactionType)
        {
            if (_interactionOnTypes.Contains(interactionType) == false)
            {
                return;
            }

            if (_active == false)
            {
                return;
            }

            if (interactionState == _peppOnState)
            {
                ShowPeppView();
            }
        }

        private void ActivatePeppObject(bool active)
        {
            _peppObject.SetActive(active, this);
        }

        private void ShowPeppView()
        {
            _guiController.SetMenuIndex(34);

            _worldObject.SetActive(false);
            _characterObject.SetActive(false);
            _peppBuildingTransform.gameObject.SetActive(true);

            transform.SetParent(_peppBuildingTransform);
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            transform.localScale = Vector3.one;

            foreach (var peppItems in _peppItems)
            {
                peppItems.SetActive(true);
            }
        }

        private void HidePeppView()
        {
            _guiController.SetMenuIndex(3);

            transform.SetParent(_initialParent);
            transform.localPosition = _initialPosition;
            transform.localRotation = _initialRotation;
            transform.localScale = _initialScale;

            _worldObject.SetActive(true);
            _characterObject.SetActive(true);
            _peppBuildingTransform.gameObject.SetActive(false);

            foreach (var peppItems in _peppItems)
            {
                peppItems.SetActive(false);
            }
        }

        public void PlacePepp(int taskIndex)
        {
            _active = false;

            TaskIndex = taskIndex;

            HidePeppView();

            ActivatePeppObject(true);

            HighlightChilds(false);

            _peppController.PeppIsActiveAtPosition(_peppObject.transform.position);
        }

        public void SetPeppInteractionActive(PeppController peppController, GameObject shopItem1, GameObject shopItem2)
        {
            _active = true;

            if (_shopItem1 != null)
            {
                Destroy(_shopItem1);
            }
            if (_shopItem2 != null)
            {
                Destroy(_shopItem2);
            }

            _shopItem1 = Instantiate(shopItem1);
            _shopItem1.transform.SetParent(_peppItems[0].transform);
            _shopItem1.transform.localPosition = Vector3.zero;
            _shopItem1.transform.localRotation = Quaternion.identity;
            //_shopItem1.transform.localScale = Vector3.one;

            _shopItem2 = Instantiate(shopItem2);
            _shopItem2.transform.SetParent(_peppItems[1].transform);
            _shopItem2.transform.localPosition = Vector3.zero;
            _shopItem2.transform.localRotation = Quaternion.identity;
            //_shopItem2.transform.localScale = Vector3.one;



            HighlightChilds(true);
        }

        public void SetPeppInactive()
        {
            _active = false;

            ActivatePeppObject(false);

            HighlightChilds(false);
        }

        private void HighlightChilds(bool highlight)
        {
            int highlightValue = highlight ? 1 : 0;

            foreach (var childMeshRenderer in transform.GetComponentsInChildren<MeshRenderer>())
            {
                childMeshRenderer.material.SetInt("_EnablePan", highlightValue);
            }
        }

        protected override void Awake()
        {

        }

        protected override void Start()
        {
            _initialParent = transform.parent;

            _initialPosition = transform.localPosition;
            _initialRotation = transform.localRotation;
            _initialScale = transform.localScale;

            foreach (var peppItems in _peppItems)
            {
                peppItems.SetActive(false);
            }
        }

        protected override void Update()
        {

        }
    }
}