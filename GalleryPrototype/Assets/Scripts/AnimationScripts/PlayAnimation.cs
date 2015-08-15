using UnityEngine;
using System.Collections;

public class PlayAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    public int _animationState;

    public void SetAnimationState(int state)
    {
        _animator.SetInteger("ID", state);
    }


}
