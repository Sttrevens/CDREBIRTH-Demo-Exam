using UnityEngine;

/// <summary>
/// Stub for the Info Terminal exam task.
/// When the player enters the trigger zone, a HUD hint appears.
/// When the player leaves, the hint disappears.
///
/// Candidates should extend this class (or write their own) to:
/// 1. Show actual terminal content on the HUD (text, images, etc.)
/// 2. Require the player to press Interact (E) while in range
/// 3. Optionally add visual feedback (material emission, sound, etc.)
///
/// Hint: reference the HUD's Text component by dragging it into the
/// inspector, or find it at runtime via GameObject.Find / FindObjectOfType.
/// </summary>
public class InfoTerminal : MonoBehaviour
{
    [Header("Interaction")]
    [Tooltip("Radius in meters — player must be within this distance.")]
    [SerializeField] private float interactionRadius = 3f;

    [Header("HUD Hint (assign in Inspector)")]
    [SerializeField] private GameObject hintText;

    /// <summary>
    /// Override this to customize what happens when the player enters range.
    /// </summary>
    protected virtual void OnPlayerEnterRange()
    {
        if (hintText != null)
            hintText.SetActive(true);
    }

    /// <summary>
    /// Override this to customize what happens when the player leaves range.
    /// </summary>
    protected virtual void OnPlayerExitRange()
    {
        if (hintText != null)
            hintText.SetActive(false);
    }

    private void Start()
    {
        if (hintText != null)
            hintText.SetActive(false);

        // Add a sphere trigger collider if none exists
        var sphere = GetComponent<SphereCollider>();
        if (sphere == null)
            sphere = gameObject.AddComponent<SphereCollider>();

        sphere.isTrigger = true;
        sphere.radius = interactionRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            OnPlayerEnterRange();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            OnPlayerExitRange();
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0f, 1f, 1f, 0.3f);
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
#endif
}
