using UnityEngine;

public class SPPlayerAnimator : MonoBehaviour
{
    private SPAnimatorManager _animatorManager;
    private Animator _animator;
    private bool _jumpTriggered;

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

        // Jump: fire trigger once per press, reset when JumpCount goes back to 0
        if (_animatorManager.JumpCount > 0 && !_jumpTriggered)
        {
            _animator.SetTrigger(JumpHash);
            _jumpTriggered = true;
        }
        if (_animatorManager.JumpCount == 0)
        {
            _jumpTriggered = false;
        }

        _animator.SetFloat(SpeedHash, _animatorManager.Speed);
    }
}
