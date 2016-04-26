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
            Hat,
            Toy
        }

        public enum State
        {
            Idle,
            Move,
            DoingQuest,
            Done
        }

        [Serializable]
        public class Quest
        {
            public QuestType QuestType;

            public GameObject QuestItemPrefab1, QuestItemPrefab2;

            public Material QuestItem1Material1, QuestItem1Material2, QuestItem2Material1, QuestItem2Material2;

            public string PeppIdA, PeppIdB;

            public string TaskADescription, TaskBDescription;

            public string QuestItemPrefabDescription1, QuestItemPrefabDescription2;

            public string QuestItemMaterialDescription1, QuestItemMaterialDescription2;

            [NonSerialized]
            public bool TaskADone, TaskBDone;

            [NonSerialized]
            public int CurrentItemObjectIndex;

            [NonSerialized]
            public GameObject CurrentItemObject;

            [NonSerialized]
            public int CurrentItemMaterialIndex;

            [NonSerialized]
            public Material CurrentItemMaterial;
        }

        [SerializeField]
        private CharacterMoveComponent _movementComponent;

        [SerializeField]
        private GameObject _questOverviewGUI;

        [SerializeField]
        private SimpleToggleGUIClass _questOverviewTaskA, _questOverviewTaskB;

        [SerializeField]
        private Transform _questItemParent;

        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private float _maxIdleMoveWaitingTime = 3;

        [SerializeField]
        private List<Quest> _quests = new List<Quest>();

        private PeppController _peppController;

        private StateMachine<State> _stateMachine;

        private int _maxTasksPerQuest = 2;

        private int _finishedTasks = 0;

        private Quest _currentQuest;

        private List<Vector3> _questTaskPositions = new List<Vector3>();

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

        public void FinishedTask(string peppId, int taskIndex)
        {
            if (_currentQuest == null)
            {
                return;
            }

            _stateMachine.ChangeState(State.Idle);

            if (peppId == _currentQuest.PeppIdA && _currentQuest.TaskADone == false)
            {
                _currentQuest.CurrentItemObjectIndex = taskIndex;// == 0 ? _currentQuest.QuestItemPrefab1 : _currentQuest.QuestItemPrefab2;
                _currentQuest.TaskADone = true;
                _questOverviewTaskA.ToggleActive = true;
                _finishedTasks++;
            }
            else if (peppId == _currentQuest.PeppIdB && _currentQuest.TaskBDone == false)
            {
                _currentQuest.CurrentItemMaterialIndex = taskIndex;// == 0 ? _currentQuest.QuestItemMaterial1 : _currentQuest.QuestItemMaterial2;
                _currentQuest.TaskBDone = true;
                _questOverviewTaskB.ToggleActive = true;
                _finishedTasks++;
            }

            if (_finishedTasks >= _maxTasksPerQuest)
            {
                StateMachine.ChangeState(State.Done);
            }

            _questTaskPositions.RemoveAt(0);

            _stateMachine.ChangeState(State.DoingQuest);
        }

        public void BreakQuest()
        {
            StateMachine.ChangeState(State.Done);

            _questTaskPositions.Clear();
        }

        public void SetQuest()
        {
            int randomQuest = UnityEngine.Random.Range(0, _quests.Count);

            _currentQuest = _quests[randomQuest];

            _questOverviewTaskA.ToggleText = _currentQuest.TaskADescription;
            _questOverviewTaskA.ToggleActive = false;
            _questOverviewTaskB.ToggleText = _currentQuest.TaskBDescription;
            _questOverviewTaskB.ToggleActive = false;
            _questOverviewGUI.SetActive(true);

            _peppController.SetPeppActivation(this, true, _currentQuest.PeppIdA, _currentQuest.QuestItemPrefabDescription1, _currentQuest.QuestItemPrefabDescription2);
            _peppController.SetPeppActivation(this, true, _currentQuest.PeppIdB, _currentQuest.QuestItemMaterialDescription1, _currentQuest.QuestItemMaterialDescription2);

            _stateMachine.ChangeState(State.Move);
        }

        private void Move_Enter()
        {
            _movementComponent.StartMovingToNextRandomPosition();
        }

        private void Move_Update()
        {
            if (_movementComponent.IsDestinationReached())
            {
                _movementComponent.StartMovingToNextRandomPosition(UnityEngine.Random.Range(0, _maxIdleMoveWaitingTime));
            }
        }

        private void Idle_Enter()
        {
            _movementComponent.StopMoving();
        }

        private void DoingQuest_Enter()
        {
            if (_questTaskPositions != null && _questTaskPositions.Count > 0)
            {
                _currentTaskPosition = _questTaskPositions[0];

                _movementComponent.StartMovingToPosition(_currentTaskPosition);
            }
            else
            {
                _stateMachine.ChangeState(State.Move);
            }
        }

        private void Done_Enter()
        {
            _questOverviewGUI.SetActive(false);

            // Add quest Item to character
            Debug.Log("Quest is done!!!");

            StateMachine.ChangeState(State.Move);
        }

        private void AddQuestItem()
        {
            switch (_currentQuest.QuestType)
            {
                case QuestType.Toy:
                case QuestType.Ballon:
                    _currentQuest.CurrentItemObject = Instantiate(_currentQuest.CurrentItemObjectIndex == 0 ? _currentQuest.QuestItemPrefab1 : _currentQuest.QuestItemPrefab2);

                    if (_currentQuest.CurrentItemObjectIndex == 0)
                    {
                        _currentQuest.CurrentItemMaterial = _currentQuest.CurrentItemMaterialIndex == 0 ? _currentQuest.QuestItem1Material1 : _currentQuest.QuestItem1Material2;
                    }
                    else
                    {
                        _currentQuest.CurrentItemMaterial = _currentQuest.CurrentItemMaterialIndex == 0 ? _currentQuest.QuestItem2Material1 : _currentQuest.QuestItem2Material2;
                    }

                    _currentQuest.CurrentItemObject.transform.SetParent(_questItemParent);
                    _currentQuest.CurrentItemObject.GetComponent<VehicleController>().DestinationObject = transform;
                    _currentQuest.CurrentItemObject.GetComponent<ItemMaterialController>().SetMaterial(_currentQuest.CurrentItemMaterial);
                    break;
                case QuestType.Hat:

                    break;
                default:
                    break;
            }
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

            _questOverviewGUI.SetActive(false);
        }

        protected override void Update()
        {

        }
    }
}