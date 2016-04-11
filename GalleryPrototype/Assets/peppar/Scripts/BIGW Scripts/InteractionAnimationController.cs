using System.Collections;
using UnityEngine;
using System.Linq;

namespace peppar
{
    public class InteractionAnimationController : BehaviourController, InteractionFunctionality
    {
        [SerializeField]
        private InteractionType[] _interactionOnTypes = new InteractionType[] { InteractionType.MouseClick, InteractionType.TouchClick, InteractionType.LookAt };

        [SerializeField]
        private InteractionState _interactionOnState = InteractionState.OnStart;

        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private string _animatorIntID;
        [SerializeField]
        private string _animatorBoolID;
        [SerializeField]
        private bool _isInSubMenu = false;

        [SerializeField]
        private int _setAnimatorIntValueTo;

        private bool _interactionOnHover = false;

        public Animator Animator
        {
            get
            {
                return _animator;
            }

            set
            {
                _animator = value;
            }
        }

        public void Interaction(InteractionState interactionState, InteractionType interactionType)
        {
            if (_interactionOnTypes.Contains(interactionType) == false)
            {
                return;
            }

            if (interactionState == _interactionOnState && _interactionOnHover == false)
            {
                ControllAnimation();

                if (interactionState == InteractionState.OnHover)
                {
                    _interactionOnHover = true;
                }
            }

            if (_interactionOnState == InteractionState.OnHover
                && interactionState == InteractionState.OnStop)
            {
                _interactionOnHover = false;
            }
        }

        private void ControllAnimation()
        {
            //if (Animator != null)
            //{
            //    Animator.SetInteger(_animatorIntID, _setAnimatorIntValueTo);
            //}

            StartCoroutine(ControllAnimationAtEndOfFrame());
        }

        private IEnumerator ControllAnimationAtEndOfFrame()
        {
            yield return new WaitForEndOfFrame();

            if (Animator != null)
            {
                Animator.SetInteger(_animatorIntID, _setAnimatorIntValueTo);

                if (_animatorBoolID != string.Empty)
                    Animator.SetBool(_animatorBoolID, _isInSubMenu);
            }
        }

        protected override void Start()
        {

        }

        protected override void Update()
        {

        }

        protected override void Awake()
        {

        }
    }
}