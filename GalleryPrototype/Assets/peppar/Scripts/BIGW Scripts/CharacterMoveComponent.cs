using UnityEngine;
using System.Collections;

namespace peppar
{
    public class CharacterMoveComponent : BehaviourController
    {
        [SerializeField]
        private float _groundLengthX = 1, _groundLengthY = 1;

        [SerializeField]
        private float _maxTimeToWave = 1, _maxTimeToMove = 3;

        [SerializeField]
        private float _destinationStoppingDistance = 0;

        [SerializeField]
        private float _nearToVuforiaCamera = 5;

        private NavMeshAgent _navMeshAgent;

        private Camera _vufCamera;

        private ObjectLookAtController _lookAtController;

        private bool _waiting = false;

        private float _currentWaitingTime, _timeToMove, _timeAfterLastWaving, _timeToWave;

        private bool _run;

        private State _state = State.None;

        private enum State
        {
            None,
            Idle,
            Moving,
            Waving,
            End
        }

        public bool Run
        {
            get
            {
                return _run;
            }
            set
            {
                _run = value;
                _navMeshAgent.enabled = value;
            }
        }

        private void StartNextAction()
        {
            if (_state != State.Idle
             && _waiting == false && IsDestinationReached())
            {
                if (NearToVuforiaCamera())
                {
                    //Wave();
                }

                Idle();
            }
            else if (_state != State.Waving
                  && _state != State.Moving
                  && _waiting && _timeAfterLastWaving > _timeToWave)
            {
                Wave();
            }
            else if (_state != State.Moving
                  && _waiting && _currentWaitingTime > _timeToMove)
            {
                Move();
            }
        }

        private void Move()
        {
            _lookAtController.enabled = false;

            // Start Moving Animation ??
            StartMovingToNextRandomPosition();
            _waiting = false;
            _currentWaitingTime = 0;

            _state = State.Moving;
        }

        private void Wave()
        {
            _lookAtController.enabled = true;

            _waiting = true;

            // Start waving animation
            _timeAfterLastWaving = 0;

            // Time needed for waving
            _timeToMove += 3;

            _state = State.Waving;
        }

        private void Idle()
        {
            _lookAtController.enabled = false;

            // Start Idle Animation
            _waiting = true;

            _timeToMove = Random.Range(0, _maxTimeToMove);
            _timeToWave = Random.Range(0, _maxTimeToWave);

            _state = State.Idle;
        }

        private bool IsDestinationReached()
        {
            // Check if we've reached the destination
            if (!_navMeshAgent.pathPending)
            {
                if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance
                    && !_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }

            return false;
        }

        private void StartMovingToNextRandomPosition()
        {
            _navMeshAgent.SetDestination(GetRandomPosition());
        }

        private Vector3 GetRandomPosition()
        {
            return new Vector3(
                Random.Range(-_groundLengthX / 2, _groundLengthX / 2),
                0,
                Random.Range(-_groundLengthY / 2, _groundLengthY / 2));
        }

        private bool NearToVuforiaCamera()
        {
            if (Vector3.Distance(transform.position, _vufCamera.transform.position) < _nearToVuforiaCamera)
            {
                return true;
            }

            return false;
        }

        protected override void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();

            _vufCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

            _lookAtController = GetComponent<ObjectLookAtController>();

            UnityEngine.Assertions.Assert.IsNotNull(_vufCamera, "CharacterMoveComponent: VuforiaCamera is null");

            UnityEngine.Assertions.Assert.IsNotNull(_navMeshAgent, "CharacterMoveComponent: NavMeshAgent is null");

            UnityEngine.Assertions.Assert.IsNotNull(_lookAtController, "CharacterMoveComponent: LookAtController is null");

            if (_lookAtController != null && _vufCamera != null)
            {
                _lookAtController.LookAtTarget = _vufCamera.transform;
            }
        }

        protected override void Start()
        {
            Run = false;

            if (_navMeshAgent != null)
            {
                _navMeshAgent.stoppingDistance = _destinationStoppingDistance;
            }
        }

        protected override void Update()
        {
            if (Run == false)
            {
                return;
            }

            if (_waiting)
            {
                _currentWaitingTime += Time.deltaTime;
                _timeAfterLastWaving += Time.deltaTime;
            }

            StartNextAction();
        }
    }
}