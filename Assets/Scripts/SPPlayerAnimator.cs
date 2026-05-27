using UnityEngine;

public class SPPlayerAnimator : MonoBehaviour
{
    private SPAnimatorManager _animatorManager;
    private Animator _animator;

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

        _animator.SetFloat(SpeedHash, _animatorManager.Speed);
        if (_animatorManager.ConsumeJumpRequest())
        {
            _animator.ResetTrigger(JumpHash);
            _animator.SetTrigger(JumpHash);
            _animator.Update(0f);
        }
    }
}
