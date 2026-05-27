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
    private float _xRotation;

    private void Awake()
    {
        FindPlayerBody();
    }

    private void Start()
    {
        if (_playerBody == null) FindPlayerBody();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (_playerBody == null) FindPlayerBody();
    }

    private void LateUpdate()
    {
        if (_playerBody == null) return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        _playerBody.Rotate(Vector3.up, mouseX);

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -lookXLimit, lookXLimit);
        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
    }

    private void FindPlayerBody()
    {
        // Walk up from camera: Head -> ... -> CharacterModel -> Player
        Transform t = transform;
        while (t != null)
        {
            if (t.TryGetComponent<CharacterController>(out _))
            {
                _playerBody = t;
                return;
            }
            t = t.parent;
        }

        // Fallback: find Player tag
        var go = GameObject.FindGameObjectWithTag("Player");
        if (go != null)
            _playerBody = go.transform;
    }
}
