using UnityEngine;

/// <summary>
/// Self-contained first-person controller using Unity's CharacterController.
/// No Photon, no networking — pure local movement for the demo exam project.
/// Requires a sibling PlayerInputHandler and a child Camera.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 4f;
    [SerializeField] private float runSpeed = 7f;
    [SerializeField] private float jumpHeight = 1.2f;
    [SerializeField] private float gravity = -9.81f;

    [Header("Look")]
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private float lookXLimit = 85f;

    [Header("References")]
    [SerializeField] private Transform cameraTransform;

    private CharacterController charController;
    private PlayerInputHandler inputHandler;

    private float verticalVelocity;
    private float xRotation;

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
        inputHandler = GetComponent<PlayerInputHandler>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleLook();
        HandleMovement();
    }

    private void HandleLook()
    {
        Vector2 lookDelta = inputHandler.Look * mouseSensitivity;

        // Horizontal rotation (entire body)
        transform.Rotate(Vector3.up, lookDelta.x);

        // Vertical rotation (camera only, clamped)
        xRotation -= lookDelta.y;
        xRotation = Mathf.Clamp(xRotation, -lookXLimit, lookXLimit);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    private void HandleMovement()
    {
        // Ground check
        bool isGrounded = charController.isGrounded;

        // Reset vertical velocity when grounded
        if (isGrounded && verticalVelocity < 0f)
            verticalVelocity = -2f; // small downward force to keep grounded

        // Horizontal movement
        Vector2 input = inputHandler.Move;
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        Vector3 move = transform.right * input.x + transform.forward * input.y;
        charController.Move(move * (speed * Time.deltaTime));

        // Jump
        if (inputHandler.JumpPressed && isGrounded)
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);

        // Apply gravity
        verticalVelocity += gravity * Time.deltaTime;
        charController.Move(Vector3.up * (verticalVelocity * Time.deltaTime));
    }
}
