﻿using UnityEngine;
namespace peppar
{
    public class ObjectLerpController : BehaviourController
    {
        [SerializeField]
        private bool _lerp;

        [SerializeField]
        private Transform _goalPosition;

        [SerializeField]
        private float _lerpSpeed = 10;

        [SerializeField]
        private float _stopLerpDistance = 0.001f;

        public bool Lerp
        {
            get
            {
                return _lerp;
            }

            set
            {
                _lerp = value;
            }
        }

        public Vector3 GoalPosition
        {
            get
            {
                if (_goalPosition != null)
                    return _goalPosition.position;
                else
                    return transform.position;
            }
        }

        public float LerpSpeed
        {
            get
            {
                return _lerpSpeed;
            }

            set
            {
                _lerpSpeed = value;
            }
        }

        public float StopLerpDistance
        {
            get
            {
                return _stopLerpDistance;
            }

            set
            {
                _stopLerpDistance = value;
            }
        }

        public Vector3 Position
        {
            get { return transform.position; }
        }

        public float DistanceToGoal
        {
            get { return Vector3.Distance(Position, GoalPosition); }
        }

        protected override void Start()
        {

        }

        protected override void Update()
        {
            if (!Lerp)
                return;

            transform.position = Vector3.Lerp(Position, GoalPosition, LerpSpeed / 100);

            if (DistanceToGoal <= StopLerpDistance)
                Lerp = false;
        }
    }
}