using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace peppar
{
    public enum Quest
    {
        Null,
        Ballon,
        Cake
    }

    public class CharacterStateMachine : BehaviourController
    {
        public enum State
        {
            Idle,
            DoingQuest,
            Done
        }

        [SerializeField]
        private CharacterMoveComponent _movementComponent;

        private StateMachine<State> _stateMachine;

        private int _maxQuestParts = 3;

        private int _finishedQuestParts = 0;

        private Quest _quest;

        public StateMachine<State> StateMachine
        {
            get
            {
                return _stateMachine;
            }

            set
            {
                _stateMachine = value;
            }
        }

        public void FinishedQuestPart(int part)
        {
            // mark quest part as done

            _finishedQuestParts++;

            if (_finishedQuestParts >= 3)
            {
                StateMachine.ChangeState(State.Done);
            }
        }

        private void SetQuest()
        {
            int randomQuest = Random.Range(0, 2);

            switch (randomQuest)
            {
                case 0:
                    _quest = Quest.Ballon;
                    break;
                case 1:
                    _quest = Quest.Cake;
                    break;
                default:
                    _quest = Quest.Ballon;
                    break;
            }

            // Show Quest Menu

            // Highlight Buildings

            // Activate pepps
        }

        private void Idle_Enter()
        {
            _quest = Quest.Null;

            _movementComponent.Run = true;
        }

        private void DoingQuest_Enter()
        {
            _movementComponent.Run = false;
        }

        private void Done_Enter()
        {
            // Hide Quest Menu

            // Add quest Item to character

            StateMachine.ChangeState(State.Idle);
        }

        protected override void Awake()
        {
            UnityEngine.Assertions.Assert.IsNotNull(_movementComponent, "CharacterStateMachine: CharacterMovementComponent is null");
        }

        protected override void Start()
        {
            StateMachine = StateMachine<State>.Initialize(this, State.Idle);

            SetQuest();
        }

        protected override void Update()
        {

        }
    }
}