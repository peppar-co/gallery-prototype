using UnityEngine;
using System.Collections;
using System;

namespace peppar
{
    public class PeppObjectController : BehaviourController
    {
        private PeppComponent _peppComponent;

        private bool _active;

        public void SetActive(bool active, PeppComponent peppComponent)
        {
            _peppComponent = peppComponent;

            _active = active;

            gameObject.SetActive(true);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(_active == false)
            {
                return;
            }

            if (other.tag == "Character")
            {
                var characterQuestMachine = other.GetComponent<CharacterQuestMachine>();

                if (characterQuestMachine != null)
                {
                    characterQuestMachine.FinishedTask(_peppComponent.PeppId);
                    _active = false;
                    gameObject.SetActive(false);
                }
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