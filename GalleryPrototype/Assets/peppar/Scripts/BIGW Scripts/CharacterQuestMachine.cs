using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace peppar
{
    public class CharacterQuestMachine : BehaviourController
    {
        public enum QuestType
        {
            Null,
            Ballon,
            Cake
        }

        public enum State
        {
            Idle,
            DoingQuest,
            Done
        }

        [Serializable]
        public class Quest
        {
            public QuestType QuestType;

            public GameObject QuestItemPrefab;

            public string PeppIdA, PeppIdB, PeppIdC;

            public string TaskADescription, TaskBDescription, TaskCDescription;

            [NonSerialized]
            public bool ShowQuestInterface = false;

            [NonSerialized]
            public bool TaskADone, TaskBDone, TaskCDone;
        }

        [SerializeField]
        private List<Quest> _quests = new List<Quest>();

        [SerializeField]
        private CharacterMoveComponent _movementComponent;

        private PeppController _peppController;

        private StateMachine<State> _stateMachine;

        private int _maxTasksPerQuest = 3;

        private int _finishedTasks = 0;

        public Quest CurrentQuest;

        private List<Vector3> _questTaskPositions;

        private Vector3 _currentTaskPosition;

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

        public void AddTaskPosition(Vector3 position)
        {
            _questTaskPositions.Add(position);

            if (_stateMachine.State != State.DoingQuest)
            {
                _stateMachine.ChangeState(State.DoingQuest);
            }
        }

        public void FinishedTask(string peppId)
        {
            if (CurrentQuest == null)
            {
                return;
            }

            if (peppId == CurrentQuest.PeppIdA && CurrentQuest.TaskADone == false)
            {
                CurrentQuest.TaskADone = true;
                _finishedTasks++;
            }
            else if (peppId == CurrentQuest.PeppIdB && CurrentQuest.TaskBDone == false)
            {
                CurrentQuest.TaskBDone = true;
                _finishedTasks++;
            }
            else if (peppId == CurrentQuest.PeppIdC && CurrentQuest.TaskCDone == false)
            {
                CurrentQuest.TaskCDone = true;
                _finishedTasks++;
            }

            if (_finishedTasks >= _maxTasksPerQuest)
            {
                StateMachine.ChangeState(State.Done);
            }
        }

        private void SetQuest()
        {
            int randomQuest = UnityEngine.Random.Range(0, 2);

            CurrentQuest = _quests[randomQuest];

            CurrentQuest.ShowQuestInterface = true;

            _peppController.SetPeppsActivation(this, true, CurrentQuest.PeppIdA, CurrentQuest.PeppIdB, CurrentQuest.PeppIdC);
        }

        private void Idle_Enter()
        {
            _movementComponent.Run = true;
        }

        private void DoingQuest_Enter()
        {
            _movementComponent.Run = false;

            while (_questTaskPositions != null && _questTaskPositions.Count > 0)
            {
                _currentTaskPosition = _questTaskPositions[0];

                if (_currentTaskPosition != _movementComponent.GetDestination())
                {
                    _movementComponent.StartMovingToPosition(_currentTaskPosition);
                }

                if (_movementComponent.IsDestinationReached())
                {
                    // DO All Pepp reached stuff ?????

                    _questTaskPositions.RemoveAt(0);
                }
            }

            _stateMachine.ChangeState(State.Idle);
        }

        private void Done_Enter()
        {
            CurrentQuest.ShowQuestInterface = false;

            // Add quest Item to character

            StateMachine.ChangeState(State.Idle);
        }

        protected override void Awake()
        {
            var gameController = GameObject.FindGameObjectWithTag("GameController");

            if (gameController != null)
            {
                _peppController = gameController.GetComponent<PeppController>();
            }

            UnityEngine.Assertions.Assert.IsNotNull(_movementComponent, "CharacterQuestMachine: CharacterMovementComponent is null");
            UnityEngine.Assertions.Assert.IsNotNull(_peppController, "CharacterQuestMachine: PeppController is null (add to GameController)");
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