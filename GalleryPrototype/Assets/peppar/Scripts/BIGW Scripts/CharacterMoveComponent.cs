using UnityEngine;
using System.Collections;

namespace peppar
{
    public class CharacterMoveComponent : BehaviourController
    {
        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private Camera _vufCamera;

        [SerializeField]
        private float _groundLengthX = 1, _groundLengthY = 1;

        [SerializeField]
        private float _maxTimeToWave = 1, _maxTimeToMove = 3;

        [SerializeField]
        private float _destinationStoppingDistance = 0;

        [SerializeField]
        private float _nearToVuforiaCamera = 5;

        private NavMeshAgent _navMeshAgent;

        private ObjectLookAtController _lookAtController;

        private bool _waiting = false;

        public bool IsDestinationReached()
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

        public Vector3 GetDestination()
        {
            return _navMeshAgent.destination;
        }

        public void StartMovingToPosition(Vector3 position)
        {
            NavMeshHit navMeshHit;
            NavMesh.SamplePosition(position, out navMeshHit, 10, NavMesh.GetAreaFromName("Flight"));

            _navMeshAgent.SetDestination(navMeshHit.position);

            _navMeshAgent.Resume();

            _lookAtController.enabled = false;
            _waiting = true;
        }

        public void StartMovingToNextRandomPosition()
        {
            Debug.Log("start Moving");
            StartMovingToPosition(GetRandomPosition());

            _waiting = false;
        }

        public void StartMovingToNextRandomPosition(float timeDelay)
        {
            Debug.Log("Want to start");

            if (_waiting == false)
            {
                StartCoroutine(StartMovingToNextRandomPositionInSeconds(timeDelay));
            }
        }

        public IEnumerator StartMovingToNextRandomPositionInSeconds(float seconds)
        {
            Debug.Log("Start Coroutine");

            _lookAtController.enabled = true;

            yield return new WaitForSeconds(seconds);

            _lookAtController.enabled = false;

            StartMovingToNextRandomPosition();
        }

        public void StopMoving()
        {
            _navMeshAgent.Stop();
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

            if (_vufCamera == null)
            {
                _vufCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            }

            _lookAtController = GetComponent<ObjectLookAtController>();

            UnityEngine.Assertions.Assert.IsNotNull(_vufCamera, "CharacterMoveComponent: VuforiaCamera is null");

            UnityEngine.Assertions.Assert.IsNotNull(_navMeshAgent, "CharacterMoveComponent: NavMeshAgent is null");

            UnityEngine.Assertions.Assert.IsNotNull(_lookAtController, "CharacterMoveComponent: LookAtController is null");

            if (_lookAtController != null && _vufCamera != null)
            {
                _lookAtController.LookAtTarget = _vufCamera.transform;

                _lookAtController.enabled = false;
            }

            //Run = false;
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
            _animator.SetFloat("Walk", _navMeshAgent.velocity.magnitude);
        }
    }
}