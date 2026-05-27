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

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _animatorManager = GetComponent<SPAnimatorManager>();
    }

    private void Start()
    {
        if (playerCamera == null) playerCamera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump")) _jumpPressed = true;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void FixedUpdate()
    {
        if (_controller.isGrounded && _verticalVelocity < 0f)
            _verticalVelocity = -2f;

        float speed = 0f;

        if (playerCamera != null)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            bool sprint = Input.GetKey(KeyCode.LeftShift);
            float currentSpeed = sprint ? runSpeed : walkSpeed;

            var forward = playerCamera.transform.forward;
            var right = playerCamera.transform.right;
            forward.y = 0f; right.y = 0f;
            forward.Normalize(); right.Normalize();
            var direction = forward * v + right * h;
            if (direction.sqrMagnitude > 1f) direction.Normalize();

            var move = direction * (currentSpeed * Time.deltaTime);
            speed = move.magnitude / Time.deltaTime;
            _controller.Move(move);
        }

        _animatorManager.Speed = speed;

        if (_jumpPressed && _controller.isGrounded)
        {
            _verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            _animatorManager.JumpCount++;
        }

        _verticalVelocity += gravity * Time.deltaTime;
        _controller.Move(Vector3.up * (_verticalVelocity * Time.deltaTime));
        _jumpPressed = false;
    }
}
