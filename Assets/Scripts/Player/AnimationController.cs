using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;

    #region UNITY EVENTS

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    #endregion

    #region PUBLIC METHODS

    public void SetDanceAnim(bool state)
    {
        _animator.SetBool("isDancing", state);
    }

    #endregion
}