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
        private GameObject _peppGUI;

        private PeppController _peppController;

        private bool _active;

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

            ShowPeppGUI();
        }

        private void ActivatePeppObject(bool active)
        {
            _peppObject.SetActive(active, this);
        }

        private void ShowPeppGUI()
        {
            if (_peppGUI != null)
            {
                _peppGUI.SetActive(true);
            }
        }

        private void HidePeppGUI()
        {
            if (_peppGUI != null)
            {
                _peppGUI.SetActive(false);
            }
        }

        public void PlacePepp(int taskIndex)
        {
            TaskIndex = taskIndex;

            ActivatePeppObject(true);

            HidePeppGUI();

            HighlightChilds(false);

            _peppController.PeppIsActiveAtPosition(_peppObject.transform.position);

            _active = false;
        }

        public void SetPeppInteractionActive(PeppController peppController, string optionDescription1, string optionDescription2)
        {
            _active = true;

            HighlightChilds(true);

            // Set description
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

        }

        protected override void Update()
        {

        }
    }
}