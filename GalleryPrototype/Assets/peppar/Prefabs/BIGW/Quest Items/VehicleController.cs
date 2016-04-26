using UnityEngine;
using System.Collections;

public class VehicleController : MonoBehaviour
{
    [SerializeField]
    public Transform DestinationObject;

    private NavMeshAgent _navMeshAgent;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (_navMeshAgent != null && DestinationObject != null)
        {
            _navMeshAgent.SetDestination(DestinationObject.position);
        }
    }
}
