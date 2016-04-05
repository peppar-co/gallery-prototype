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

        private NavMeshAgent _navMeshAgent;

        private bool _waiting = false;

        private float _currentWaitingTime, _timeToMove, _timeAfterLastWaving, _timeToWave;

        private void StartNextAction()
        {
            if (_waiting == false && IsDestinationReached())
            {
                Idle();
            }

            if (_waiting && _timeAfterLastWaving > _timeToWave)
            {
                Wave();
            }

            if (_waiting && _currentWaitingTime > _timeToMove)
            {
                Move();
            }
        }

        private void Move()
        {
            // Start Moving Animation ??
            StartMovingToNextRandomPosition();
            _waiting = false;
            _currentWaitingTime = 0;
        }

        private void Wave()
        {
            // Start waving animation
            _timeAfterLastWaving = 0;

            _timeToWave = Random.Range(0, _maxTimeToWave);
        }

        private void Idle()
        {
            // Start Idle Animation
            _waiting = true;

            _timeToMove = Random.Range(0, _maxTimeToMove);
            _timeToWave = Random.Range(0, _maxTimeToWave);
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

        protected override void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();

            UnityEngine.Assertions.Assert.IsNull(_navMeshAgent, "CharacterMoveComponent: NavMeshAgent is null");
        }

        protected override void Start()
        {
            if (_navMeshAgent != null)
            {
                _navMeshAgent.stoppingDistance = _destinationStoppingDistance;
            }
        }

        protected override void Update()
        {
            if (_waiting)
            {
                _currentWaitingTime += Time.deltaTime;
                _timeAfterLastWaving += Time.deltaTime;
            }

            StartNextAction();
        }
    }
}