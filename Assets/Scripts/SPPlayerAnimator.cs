using UnityEngine;

/// <summary>
/// Simple animation driver — reads Speed from SPAnimatorManager and feeds it to Animator.
/// Designed for SimpleLocomotion.controller (Idle <-> Walk <-> Run via Speed parameter).
/// </summary>
public class SPPlayerAnimator : MonoBehaviour
{
    private SPAnimatorManager _animatorManager;
    private Animator _animator;
    private int _lastVisibleJump;

    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    private static readonly int JumpHash = Animator.StringToHash("Jump");

    private void Awake()
    {
        _animatorManager = GetComponent<SPAnimatorManager>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        if (_animatorManager == null) _animatorManager = GetComponent<SPAnimatorManager>();
        if (_animator == null) _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (_animatorManager == null || _animator == null) return;

        if (_lastVisibleJump < _animatorManager.JumpCount)
        {
            _animator.SetTrigger(JumpHash);
        }
        _lastVisibleJump = _animatorManager.JumpCount;

        _animator.SetFloat(SpeedHash, _animatorManager.Speed);
    }
}
