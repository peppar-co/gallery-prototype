using UnityEngine;
using System.Collections;

public class BIGWGUIController : MonoBehaviour
{
    [SerializeField]
    private Animator GUIStateMachine;

    public void IdleMenu()
    {
        GUIStateMachine.SetInteger("MenuIndex", 0);
    }
    public void CharacterMenu()
    {
        GUIStateMachine.SetInteger("MenuIndex", 1);
    }
    public void FaceMenu()
    {
        GUIStateMachine.SetInteger("MenuIndex", 2);
    }
    public void NameMenu()
    {
        GUIStateMachine.SetInteger("MenuIndex", 3);
    }
}
