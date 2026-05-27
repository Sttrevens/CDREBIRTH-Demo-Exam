using UnityEngine;

/// <summary>
/// Single-player movement controller using Unity CharacterController.
/// Designed for the Demo Exam project — no Photon/Fusion dependency.
/// Requires: CharacterController, SPPlayerAnimator, SPAnimatorManager components.
/// Camera child must have FirstPersonCamera component.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class SPPlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 4f;
    [SerializeField] private float runSpeed = 7f;
    [SerializeField] private float jumpHeight = 1.2f;
    [SerializeField] private float gravity = -9.81f;

    [Header("References")]
    [SerializeField] private Camera playerCamera;

    private CharacterController controller;
    private SPAnimatorManager animatorManager;
    private SPPlayerAnimator playerAnimator;

    private float verticalVelocity;
    private bool jumpPressed;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animatorManager = GetComponent<SPAnimatorManager>();
        playerAnimator = GetComponent<SPPlayerAnimator>();
    }

    private void Start()
    {
        if (playerCamera == null)
            playerCamera = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
            jumpPressed = true;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void FixedUpdate()
    {
        // Ground check
        if (controller.isGrounded && verticalVelocity < 0f)
            verticalVelocity = -2f;

        // Raw input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Camera-relative movement vector
        Vector3 move = Vector3.zero;
        float speed = 0f;

        if (playerCamera != null)
        {
            bool sprint = Input.GetKey(KeyCode.LeftShift);
            float currentSpeed = sprint ? runSpeed : walkSpeed;

            Vector3 forward = playerCamera.transform.forward;
            Vector3 right = playerCamera.transform.right;
            forward.y = 0; right.y = 0;
            forward.Normalize(); right.Normalize();
            Vector3 direction = forward * vertical + right * horizontal;
            if (direction.sqrMagnitude > 1f) direction.Normalize();

            move = direction * (currentSpeed * Time.deltaTime);
            speed = move.magnitude / Time.deltaTime;

            controller.Move(move);

            // Feed animator: Speed drives overall motion rate;
            // XAxis/ZAxis drive directional blend tree (Walk Fwd/Back/Left/Right)
            animatorManager.Speed = speed;
            animatorManager.XAxis = horizontal;
            animatorManager.ZAxis = vertical;
        }

        // Jump
        if (jumpPressed && controller.isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animatorManager.JumpCount++;
        }

        // Gravity
        verticalVelocity += gravity * Time.deltaTime;
        controller.Move(Vector3.up * (verticalVelocity * Time.deltaTime));

        jumpPressed = false;
    }
}
