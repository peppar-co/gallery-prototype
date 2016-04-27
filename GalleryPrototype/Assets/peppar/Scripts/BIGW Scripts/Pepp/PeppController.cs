using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace peppar
{
    public class PeppController : BehaviourController
    {
        [SerializeField]
        private List<PeppComponent> _pepps = new List<PeppComponent>();

        private CharacterQuestMachine _characterQuestMachine;

        public void SetPeppActivation(CharacterQuestMachine characterQuestMachine, bool active, string peppId, GameObject shopItem1, GameObject shopItem2)
        {
            _characterQuestMachine = characterQuestMachine;

            foreach (var pepp in _pepps)
            {
                if (pepp.PeppId == peppId)
                {
                    pepp.SetPeppInteractionActive(this, shopItem1, shopItem2);
                }
            }
        }

        public void SetAllPeppsInactive()
        {
            foreach(var pepp in _pepps)
            {
                pepp.SetPeppInactive();
            }
        }

        public void PeppIsActiveAtPosition(Vector3 position)
        {
            if(_characterQuestMachine != null)
            {
                _characterQuestMachine.AddTaskPosition(position);
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