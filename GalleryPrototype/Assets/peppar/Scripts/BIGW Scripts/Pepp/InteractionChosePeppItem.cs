using UnityEngine;
using System.Collections;
using System.Linq;

namespace peppar
{
    public class InteractionChosePeppItem : BehaviourController, InteractionFunctionality
    {

        [SerializeField]
        private InteractionType[] _interactionOnTypes = new InteractionType[] { InteractionType.MouseClick, InteractionType.TouchClick, InteractionType.LookAt };

        [SerializeField]
        private InteractionState _choseItemOnState = InteractionState.OnStart;

        [SerializeField]
        private PeppComponent _peppComponent;

        [SerializeField]
        private int _itemIndex;

        public void Interaction(InteractionState interactionState, InteractionType interactionType)
        {
            if (_interactionOnTypes.Contains(interactionType) == false)
            {
                return;
            }

            if (interactionState == _choseItemOnState)
            {
                ChoseItem();
            }
        }

        private void ChoseItem()
        {
            if (_peppComponent == null)
            {
                return;
            }

            _peppComponent.PlacePepp(_itemIndex);
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
