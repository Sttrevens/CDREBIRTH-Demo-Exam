using UnityEngine;

public class SPPlayerAnimator : MonoBehaviour
{
    // PRIVATE MEMBERS
    private SPAnimatorManager _animatorManager;
    private Animator _animator;
    private int _lastVisibleJump;
    private int _lastVisiblePickup;
    private int _lastVisibleWield;
    private int _lastVisibleTwoHandWield;

    // MONOBEHAVIOUR
    protected void Awake()
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
        UpdateAnimations();
    }

    // PRIVATE METHODS
    private void UpdateAnimations()
    {
        if (_lastVisibleJump < _animatorManager.JumpCount)
        {
            _animator.SetTrigger("Jump");
        }
        _lastVisibleJump = _animatorManager.JumpCount;

        if (_lastVisiblePickup < _animatorManager.PickupCount)
        {
            _animator.SetTrigger("Pickup");
        }
        _lastVisiblePickup = _animatorManager.PickupCount;

        if (_lastVisibleWield < _animatorManager.WieldCount)
        {
            _animator.SetTrigger("OneHandAttack");
        }
        _lastVisibleWield = _animatorManager.WieldCount;

        if (_lastVisibleTwoHandWield < _animatorManager.TwoHandWieldCount)
        {
            _animator.SetTrigger("TwoHandAttack");
        }
        _lastVisibleTwoHandWield = _animatorManager.TwoHandWieldCount;

        _animator.SetFloat("Speed", _animatorManager.Speed);
        _animator.SetFloat("XAxis", _animatorManager.XAxis);
        _animator.SetFloat("ZAxis", _animatorManager.ZAxis);
    }
}
