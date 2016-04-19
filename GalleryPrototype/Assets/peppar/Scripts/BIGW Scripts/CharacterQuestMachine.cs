using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace peppar
{
    public class CharacterStateMachine : BehaviourController
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

            public bool ShowQuestInterface = false;

            public GameObject QuestItemPrefab;

            public string PeppIdA, PeppIdB, PeppIdC;

            public string TaskA, TaskB, TaskC;

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

        private Quest _currentQuest;

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

        public void FinishedTask(int task)
        {
            if (_currentQuest == null)
            {
                return;
            }

            if (task == 1 && _currentQuest.TaskADone == false)
            {
                _currentQuest.TaskADone = true;
                _finishedTasks++;
            }
            else if (task == 2 && _currentQuest.TaskBDone == false)
            {
                _currentQuest.TaskBDone = true;
                _finishedTasks++;
            }
            else if (task == 3 && _currentQuest.TaskCDone == false)
            {
                _currentQuest.TaskCDone = true;
                _finishedTasks++;
            }

            if (_finishedTasks >= _maxTasksPerQuest)
            {
                StateMachine.ChangeState(State.Done);
            }
        }

        public void SetTask(int task)
        {
            if (_currentQuest == null)
            {
                return;
            }

            if (task == 1 && _currentQuest.TaskADone == false)
            {
                _currentQuest.TaskADone = true;

                _peppController.SetPeppBuildingHighlighting(0, _currentQuest.PeppIdA);
                _peppController.SetPeppsActivation(true, _currentQuest.PeppIdA);

                _finishedTasks++;
            }
            else if (task == 2 && _currentQuest.TaskBDone == false)
            {
                _currentQuest.TaskBDone = true;

                _peppController.SetPeppBuildingHighlighting(0, _currentQuest.PeppIdB);
                _peppController.SetPeppsActivation(true, _currentQuest.PeppIdB);

                _finishedTasks++;
            }
            else if (task == 3 && _currentQuest.TaskCDone == false)
            {
                _currentQuest.TaskCDone = true;

                _peppController.SetPeppBuildingHighlighting(0, _currentQuest.PeppIdC);
                _peppController.SetPeppsActivation(true, _currentQuest.PeppIdC);

                _finishedTasks++;
            }
        }

        private void SetQuest()
        {
            int randomQuest = UnityEngine.Random.Range(0, 2);

            _currentQuest = _quests[randomQuest];

            _currentQuest.ShowQuestInterface = true;

            _peppController.SetPeppBuildingHighlighting(1, _currentQuest.PeppIdA, _currentQuest.PeppIdB, _currentQuest.PeppIdC);
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
                    // DO All Pepp reached stuff !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

                    _questTaskPositions.RemoveAt(0);
                }
            }

            _stateMachine.ChangeState(State.Idle);
        }

        private void Done_Enter()
        {
            _currentQuest.ShowQuestInterface = false;

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