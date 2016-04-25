using System;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

namespace peppar
{
    public class PepparTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
    {
        public Action<Transform> TrackingFound;
        public Action<Transform, TrackableBehaviour.Status> TrackingFoundDetailed;
        public Action<Transform> TrackingLost;

        [SerializeField]
        private bool _debug = false;

        [SerializeField]
        [Tooltip("true: set complete object inactive; false: turn off renderer and collider")]
        private bool _onlyControllObjectActivation = false;

        [SerializeField]
        private List<GameObject> _dependentObjects = new List<GameObject>();

        private TrackableBehaviour mTrackableBehaviour;

        public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
        {
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                OnTrackingFound(newStatus);
            }
            else
            {
                OnTrackingLost();
            }
        }

        private void OnTrackingFound(TrackableBehaviour.Status newStatus)
        {
            if (_onlyControllObjectActivation == false)
            {
                ChangeObjectStatus(transform, true);
            }

            ChangeDependentObjectStatus(true);

            if (TrackingFound != null)
            {
                TrackingFound(transform);
            }

            if (TrackingFoundDetailed != null)
            {
                TrackingFoundDetailed(transform, newStatus);
            }

            if (_debug || PepparManager.GameController.Debug)
            {
                Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
            }
        }

        private void OnTrackingLost()
        {
            if (_onlyControllObjectActivation == false)
            {
                ChangeObjectStatus(transform, false);
            }

            ChangeDependentObjectStatus(false);

            if (TrackingLost != null)
            {
                TrackingLost(transform);
            }

            if (_debug || PepparManager.GameController.Debug)
            {
                Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
            }
        }

        private void ChangeObjectStatus(Transform change, bool active)
        {
            if (_onlyControllObjectActivation)
            {
                //Transform[] transformComponents = change.GetComponentsInChildren<Transform>(true);

                //foreach (Transform component in transformComponents)
                //{
                //    component.gameObject.SetActive(active);
                //}

                change.gameObject.SetActive(active);
            }
            else
            {
                Renderer[] rendererComponents = change.GetComponentsInChildren<Renderer>(true);
                Collider[] colliderComponents = change.GetComponentsInChildren<Collider>(true);

                // Enable rendering:
                foreach (Renderer component in rendererComponents)
                {
                    component.enabled = active;
                }

                // Enable colliders:
                foreach (Collider component in colliderComponents)
                {
                    component.enabled = active;
                }
            }
        }

        private void ChangeDependentObjectStatus(bool active)
        {
            if (_dependentObjects == null)
            {
                return;
            }

            foreach (GameObject dependentObject in _dependentObjects)
            {
                if (dependentObject != null)
                {
                    ChangeObjectStatus(dependentObject.transform, active);
                }
            }
        }

        private void Start()
        {
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }
        }
    }
}