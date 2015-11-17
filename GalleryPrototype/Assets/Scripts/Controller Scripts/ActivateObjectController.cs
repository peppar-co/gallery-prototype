using UnityEngine;
using System.Collections;

public class ActivateObjectController : MonoBehaviour
{
    [SerializeField]
    private bool _activeOnStart;

    public bool IsActive { get { return gameObject.activeSelf; } }

    // Use this for initialization
    private void Start()
    {
        gameObject.SetActive(_activeOnStart);
    }

    public void ChangeActivationStatus(bool active)
    {
        gameObject.SetActive(active);
    }

    [ContextMenu("ChangeActivationStatus True")]
    public void ChangeActivationStatusTrue()
    {
        ChangeActivationStatus(true);
    }

    [ContextMenu("ChangeActivationStatus False")]
    public void ChangeActivationStatusFalse()
    {
        ChangeActivationStatus(false);
    }
}
