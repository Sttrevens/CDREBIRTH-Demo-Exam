using UnityEngine;

/// <summary>
/// Minimal first-person camera with mouse look. No networking.
/// Attach to a child Camera of the Player.
/// </summary>
public class FirstPersonCamera : MonoBehaviour
{
    [Header("Look Settings")]
    public float mouseSensitivity = 2f;
    public float lookXLimit = 85f;

    private float xRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Horizontal rotation on the parent (player body)
        transform.parent.Rotate(Vector3.up, mouseX);

        // Vertical rotation on the camera itself
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -lookXLimit, lookXLimit);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
