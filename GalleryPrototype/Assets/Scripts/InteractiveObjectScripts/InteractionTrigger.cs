using UnityEngine;
using System.Collections;

public class InteractionTrigger : MonoBehaviour
{
    [SerializeField]
    private SphereCoreObjectController _sphereCore;



    private void OnMouseDown()
    {
        TriggerInteraction();
    }


    private void TriggerInteraction()
    {
        _sphereCore.IsActive = !_sphereCore.IsActive;
        //_sphereCore.gameObject.SetActive(_sphereCore.IsActive);
    }

}
