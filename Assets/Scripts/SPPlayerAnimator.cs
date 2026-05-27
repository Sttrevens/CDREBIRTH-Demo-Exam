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

    // Parameter hashes (cache for performance)
    private static readonly int JumpHash = Animator.StringToHash("Jump");
    private static readonly int PickupHash = Animator.StringToHash("Pickup");
    private static readonly int OneHandAttackHash = Animator.StringToHash("OneHandAttack");
    private static readonly int TwoHandAttackHash = Animator.StringToHash("TwoHandAttack");
    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    private static readonly int XAxisHash = Animator.StringToHash("XAxis");
    private static readonly int ZAxisHash = Animator.StringToHash("ZAxis");

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
            _animator.SetTrigger(JumpHash);
        }
        _lastVisibleJump = _animatorManager.JumpCount;

        if (_lastVisiblePickup < _animatorManager.PickupCount)
        {
            _animator.SetTrigger(PickupHash);
        }
        _lastVisiblePickup = _animatorManager.PickupCount;

        if (_lastVisibleWield < _animatorManager.WieldCount)
        {
            _animator.SetTrigger(OneHandAttackHash);
        }
        _lastVisibleWield = _animatorManager.WieldCount;

        if (_lastVisibleTwoHandWield < _animatorManager.TwoHandWieldCount)
        {
            _animator.SetTrigger(TwoHandAttackHash);
        }
        _lastVisibleTwoHandWield = _animatorManager.TwoHandWieldCount;

        _animator.SetFloat(SpeedHash, _animatorManager.Speed);
        _animator.SetFloat(XAxisHash, _animatorManager.XAxis);
        _animator.SetFloat(ZAxisHash, _animatorManager.ZAxis);
    }
}
