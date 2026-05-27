using UnityEngine;

public class SPAnimatorManager : MonoBehaviour
{
    public float Speed { get; set; }
    public bool IsGrounded { get; set; }

    private bool _jumpRequested;

    public void RequestJump()
    {
        _jumpRequested = true;
    }

    public bool ConsumeJumpRequest()
    {
        if (!_jumpRequested) return false;
        _jumpRequested = false;
        return true;
    }
}
