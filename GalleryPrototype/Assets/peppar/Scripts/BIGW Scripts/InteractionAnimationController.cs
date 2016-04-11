using System.Collections;
using UnityEngine;

namespace peppar
{
    public class InteractionAnimationController : BehaviourController, InteractionFunctionality
    {
        [SerializeField]
        private InteractionState _setIntOnState = InteractionState.Start;

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
            if (interactionState == _setIntOnState)
            {
                ControllAnimation();
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