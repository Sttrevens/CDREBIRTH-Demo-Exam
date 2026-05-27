using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Wraps the PlayerControls InputActionAsset and exposes
/// Move, Look, Jump, and Interact as simple public fields.
/// Attach this alongside FirstPersonController on the Player GameObject.
/// </summary>
public class PlayerInputHandler : MonoBehaviour
{
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset inputActions;

    // Public read-only accessors for the movement controller
    public Vector2 Move { get; private set; }
    public Vector2 Look { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool InteractPressed { get; private set; }

    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;
    private InputAction interactAction;

    private void Awake()
    {
        var playerMap = inputActions.FindActionMap("Player");
        moveAction    = playerMap.FindAction("Move");
        lookAction    = playerMap.FindAction("Look");
        jumpAction    = playerMap.FindAction("Jump");
        interactAction = playerMap.FindAction("Interact");

        // Consume the Jump button so it doesn't remain true across frames
        jumpAction.performed += _ => JumpPressed = true;
        jumpAction.canceled  += _ => JumpPressed = false;

        interactAction.performed += _ => InteractPressed = true;
        interactAction.canceled  += _ => InteractPressed = false;
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Update()
    {
        Move = moveAction.ReadValue<Vector2>();
        Look = lookAction.ReadValue<Vector2>();
    }

    private void LateUpdate()
    {
        // Consume one-shot interact after controllers have had a chance to read it
        InteractPressed = interactAction.IsPressed();
    }
}
