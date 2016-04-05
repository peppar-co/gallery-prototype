﻿using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

namespace peppar
{
    public class CharacterCreator : BehaviourController
    {
        [SerializeField]
        private GameObject _characterPrefab;

        [SerializeField]
        private Transform _creationPosition, _startMovingPosition;

        [SerializeField]
        private Text _nameText;

        private GameObject _currentCharacterObject;

        private CharacterComponent _currentCharacterComponent;

        private CharacterMoveComponent _currentCharacterMoveComponent;

        public void StartCharacterCreation()
        {
            _currentCharacterObject = Instantiate(_characterPrefab);
            _currentCharacterComponent = _currentCharacterObject.GetComponent<CharacterComponent>();
            _currentCharacterObject.transform.position = _creationPosition.position;
        }

        public void CancelCharacterCreation()
        {
            Destroy(_currentCharacterObject);
        }

        public void FinishCharacterCreation()
        {
            _currentCharacterObject.transform.position = _startMovingPosition.position;
            _currentCharacterMoveComponent.Run = true;
        }

        public void SetFace()
        {
            //_currentCharacterComponent.SetFace();
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

        }

        protected override void Update()
        {

        }
    }
}