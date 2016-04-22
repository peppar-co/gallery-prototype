using UnityEngine;
using System.Collections;
using System.Linq;

namespace peppar
{
    public class InteractionChangeCharacterModule : BehaviourController, InteractionFunctionality
    {
        [SerializeField]
        private InteractionType[] _interactionOnTypes = new InteractionType[] { InteractionType.MouseClick, InteractionType.TouchClick, InteractionType.LookAt };

        [SerializeField]
        private InteractionState _changeClothOnState = InteractionState.OnStart;

        [SerializeField]
        private CharacterCustomizer _charaterCustomizer;

        [SerializeField]
        private CharacterModuleType _moduleType;

        [SerializeField]
        private int _moduleIndex;

        public void Interaction(InteractionState interactionState, InteractionType interactionType)
        {
            if (_interactionOnTypes.Contains(interactionType) == false)
            {
                return;
            }

            if (interactionState == _changeClothOnState)
            {
                ChangeModule();
            }
        }

        private void ChangeModule()
        {
            if(_charaterCustomizer == null)
            {
                return;
            }

            _charaterCustomizer.SetCharacterModule(_moduleType, _moduleIndex);
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