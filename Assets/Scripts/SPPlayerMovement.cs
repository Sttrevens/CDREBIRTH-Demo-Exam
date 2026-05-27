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

        // Movement input (old-style Input for simplicity in demo)
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        // Camera-relative movement
        if (playerCamera != null)
        {
            Vector3 forward = playerCamera.transform.forward;
            Vector3 right = playerCamera.transform.right;
            forward.y = 0; right.y = 0;
            forward.Normalize(); right.Normalize();
            Vector3 move = (forward * input.z + right * input.x) * (speed * Time.deltaTime);
            controller.Move(move);
            animatorManager.Speed = move.magnitude / Time.deltaTime;
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
