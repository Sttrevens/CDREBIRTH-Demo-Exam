using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SPPlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 10f;
    [SerializeField] private float runSpeed = 15f;
    [SerializeField] private float jumpHeight = 1.2f;
    [SerializeField] private float gravity = -9.81f;

    [Header("References")]
    [SerializeField] private Camera playerCamera;

    private CharacterController _controller;
    private SPAnimatorManager _animatorManager;

    private float _verticalVelocity;
    private bool _jumpPressed;
    private bool _isJumping;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _animatorManager = GetComponent<SPAnimatorManager>();
    }

    private void Start()
    {
        if (playerCamera == null) playerCamera = GetComponentInChildren<Camera>();
        SPCursorLock.Lock();
    }

    private void Update()
    {
        if (!SPCursorLock.HandleInput())
        {
            _jumpPressed = false;
            return;
        }

        if (Input.GetButtonDown("Jump")) _jumpPressed = true;

        if (_jumpPressed && _controller.isGrounded && !_isJumping)
        {
            _verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            _isJumping = true;
            _animatorManager.RequestJump();
        }
        _jumpPressed = false;
    }

    private void FixedUpdate()
    {
        if (_controller.isGrounded && _verticalVelocity < 0f)
        {
            _verticalVelocity = -2f;
            if (_isJumping)
            {
                _isJumping = false;
            }
        }

        _animatorManager.IsGrounded = _controller.isGrounded;

        float speed = 0f;
        if (playerCamera != null && SPCursorLock.IsLocked)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            bool sprint = Input.GetKey(KeyCode.LeftShift);
            float currentSpeed = sprint ? runSpeed : walkSpeed;

            Vector3 forward = playerCamera.transform.forward;
            Vector3 right = playerCamera.transform.right;
            forward.y = 0f; right.y = 0f;
            if (forward.sqrMagnitude < 0.001f) forward = Vector3.forward;
            if (right.sqrMagnitude < 0.001f) right = Vector3.right;
            forward.Normalize(); right.Normalize();

            var direction = forward * v + right * h;
            if (direction.sqrMagnitude > 1f) direction.Normalize();

            var move = direction * (currentSpeed * Time.deltaTime);
            speed = move.magnitude / Time.deltaTime;
            _controller.Move(move);
        }

        _animatorManager.Speed = speed;
        _verticalVelocity += gravity * Time.deltaTime;
        _controller.Move(Vector3.up * (_verticalVelocity * Time.deltaTime));
    }
}
