using UnityEngine;

/// <summary>
/// Minimal first-person camera with mouse look. No networking.
/// Mouse X rotates the player body (horizontal); Mouse Y rotates the camera (vertical).
/// Camera should be parented to a bone (e.g. Head) for proper first-person alignment.
/// </summary>
public class FirstPersonCamera : MonoBehaviour
{
    [Header("Look Settings")]
    public float mouseSensitivity = 2f;
    public float lookXLimit = 85f;

    private Transform _playerBody;
    private float xRotation;

    private void Awake()
    {
        FindPlayerBody();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        // Ensure playerBody ref is valid each frame
        if (_playerBody == null) FindPlayerBody();

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Horizontal: rotate the player body
        if (_playerBody != null)
            _playerBody.Rotate(Vector3.up, mouseX);

        // Vertical: tilt camera locally
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -lookXLimit, lookXLimit);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    private void FindPlayerBody()
    {
        // Walk up from camera to find the GameObject with CharacterController
        var t = transform;
        while (t != null)
        {
            if (t.TryGetComponent<CharacterController>(out _))
            {
                _playerBody = t;
                return;
            }
            t = t.parent;
        }
        // Fallback: find by tag
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) _playerBody = player.transform;
    }
}
