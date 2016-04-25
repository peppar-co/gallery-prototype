using UnityEngine;
using System.Collections;
using System;

namespace peppar
{
    public class PeppComponent : BehaviourController, InteractionFunctionality
    {
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
        private GUIQuestChoiceController _guiQuestChoiceController;

        [SerializeField]
        private PeppController _peppController;

        private bool _active;

        private Transform _initialParent;

        private Vector3 _initialPosition, _initialScale;

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
            if (_active == false)
            {
                return;
            }

            ShowPeppView();
        }

        private void ActivatePeppObject(bool active)
        {
            _peppObject.SetActive(active, this);
        }

        private void ShowPeppView()
        {
            _worldObject.SetActive(false);
            _characterObject.SetActive(false);
            _peppBuildingTransform.gameObject.SetActive(true);

            transform.SetParent(_peppBuildingTransform);
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            transform.localScale = Vector3.one;

            foreach(var peppItems in _peppItems)
            {
                peppItems.SetActive(true);
            }
        }

        private void HidePeppView()
        {
            _worldObject.SetActive(true);
            _characterObject.SetActive(true);
            _peppBuildingTransform.gameObject.SetActive(false);

            transform.SetParent(_initialParent);
            transform.position = _initialPosition;
            transform.rotation = _initialRotation;
            transform.localScale = _initialScale;

            foreach (var peppItems in _peppItems)
            {
                peppItems.SetActive(false);
            }
        }

        public void PlacePepp(int taskIndex)
        {
            TaskIndex = taskIndex;

            ActivatePeppObject(true);

            HidePeppView();

            HighlightChilds(false);

            _peppController.PeppIsActiveAtPosition(_peppObject.transform.position);

            _active = false;
        }

        public void SetPeppInteractionActive(PeppController peppController, string optionDescription1, string optionDescription2)
        {
            _active = true;

            HighlightChilds(true);

            _guiQuestChoiceController.SetChoiceButtonLabels(optionDescription1, optionDescription2);
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

            _initialPosition = transform.position;
            _initialRotation = transform.rotation;
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