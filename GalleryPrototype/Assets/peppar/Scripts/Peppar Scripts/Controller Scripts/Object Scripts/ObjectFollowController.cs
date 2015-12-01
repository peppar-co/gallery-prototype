using UnityEngine;

namespace peppar
{
    public class ObjectFollowController : BehaviourController
    {
        [SerializeField]
        private bool _followPosition = true;

        [SerializeField]
        private bool _followRotation = true;

        [SerializeField]
        private Transform _followTransform;

        public bool FollowPosition
        {
            get
            {
                return _followPosition;
            }

            set
            {
                _followPosition = value;
            }
        }

        public bool FollowRotation
        {
            get
            {
                return _followRotation;
            }

            set
            {
                _followRotation = value;
            }
        }

        public Transform FollowTransform
        {
            get
            {
                return _followTransform;
            }

            set
            {
                _followTransform = value;
            }
        }

        public void StartFollowing()
        {
            FollowPosition = true;
            FollowRotation = true;
        }

        public void StopFollowing()
        {
            FollowPosition = false;
            FollowRotation = false;
        }

        private void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        private void SetRotation(Quaternion rotation)
        {
            transform.rotation = rotation;
        }

        protected override void Start()
        {
        }

        protected override void Update()
        {
            if (_followTransform == null)
                return;

            if (_followPosition)
                SetPosition(_followTransform.position);

            if (_followRotation)
                SetRotation(_followTransform.rotation);
        }
    }
}